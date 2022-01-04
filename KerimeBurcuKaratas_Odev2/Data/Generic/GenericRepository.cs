using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Generic
{
    //GenericRepository için class eklendi. (Repository pattern için)
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ILogger logger;
        protected ApplicationDbContext context;
        internal DbSet<T> dbSet;


        public GenericRepository(ApplicationDbContext context, ILogger logger)
        {
            this.logger = logger;
            this.context = context;

            dbSet = context.Set<T>();
        }


        public async Task<bool> Add(T entity)
        {
            var result = await dbSet.AddAsync(entity);
            return true; //kontrol edilecek.
        }

        public async Task<bool> Delete(long id)
        {
            var entity = await dbSet.FindAsync(id);
            var result = dbSet.Remove(entity);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            // get all
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(long id)
        {
            var model = await dbSet.FindAsync(id);
            return model;
        }

        public async Task<bool> Update(T entity)
        {

            var result = dbSet.Update(entity);
            return true;
        }
    }
}
