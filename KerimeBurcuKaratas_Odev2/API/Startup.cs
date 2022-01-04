using API.Common;
using Data.Context;
using Data.Uow;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //json loop'unu iptal etmek için kullanýldý.
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //AutoMapper kullanýmý için eklendi.
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllers();

            //appsettings.json'daki ConnectionStrings'da "DefaultConnection" ismindeki baðlantý adresi deðiþkene atandý. 
            var dbConnection = Configuration.GetConnectionString("DefaultConnection");

            //SqlServer kullanmak için gerekli kod eklendi. ApplicationDbContext'deki Constractordaki options parametresi.
            services.AddDbContext<ApplicationDbContext>(options =>

                    options.UseSqlServer(dbConnection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

            );
            //Proje derlendiðinde UnitOfWork çalýþmasý için eklendi.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
