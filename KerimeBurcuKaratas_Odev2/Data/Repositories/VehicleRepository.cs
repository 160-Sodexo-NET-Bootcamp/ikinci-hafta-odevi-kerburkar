using Data.Context;
using Data.DataModel;
using Data.Generic;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    //Vehicle için repository eklendi. GenericRepository miras verildi.
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        //Verilen id ile vehicle ve container çekmek için kullanıldı.
        public async Task<Vehicle> GetById(long id)
        {
            var vehicle = await context.Vehicle.Include(v => v.Containers).FirstOrDefaultAsync(v=>v.Id == id);
            return vehicle == null ? null : vehicle;
        }
    }
}
