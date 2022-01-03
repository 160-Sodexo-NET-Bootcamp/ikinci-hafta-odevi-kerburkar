using Data.DataModel;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    //Vehicle için Repository patern uygulandı. (GenericRepository)
    public interface IVehicleRepository:IGenericRepository<Vehicle>
    {
    }
}
