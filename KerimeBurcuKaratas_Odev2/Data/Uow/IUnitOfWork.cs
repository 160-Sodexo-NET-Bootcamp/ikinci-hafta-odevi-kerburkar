using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Uow
{
    //Repositoryleri kullanmak için interface oluşturuldu.
    public interface IUnitOfWork
    {
        IVehicleRepository Vehicle { get; }
        IContainerRepository Container { get; }

        int Complete();
    }
}
