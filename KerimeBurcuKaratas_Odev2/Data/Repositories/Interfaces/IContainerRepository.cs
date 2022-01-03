using Data.DataModel;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    //Container için Repository patern uygulandı. (GenericRepository)
    public interface IContainerRepository:IGenericRepository<Container>
    {
    }
}
