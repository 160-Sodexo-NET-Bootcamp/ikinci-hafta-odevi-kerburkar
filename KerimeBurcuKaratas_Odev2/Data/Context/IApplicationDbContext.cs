using Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    
    public interface IApplicationDbContext
    {
        //Sınıf ile veri tabanındaki tablo ilişkisi kuruldu.
        DbSet<Vehicle> Vehicle { get; set; }
        DbSet<Container> Container { get; set; }

        //Bu metot sorguların tamamlanmasını sağlar. 
        int SaveChanges();
    }
}
