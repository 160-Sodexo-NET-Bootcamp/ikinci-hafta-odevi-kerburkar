using API.Dtos;
using API.Model;
using AutoMapper;
using Data.DataModel;
using Data.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VehicleController> _logger;
        private readonly IMapper _mapper;
        public VehicleController(IUnitOfWork unitOfWork,ILogger<VehicleController> logger, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Tüm vehicle listesini göstermek için kullanıldı.
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.Vehicle.GetAll();
            var map = _mapper.Map<IEnumerable<VehicleDto>>(result);
            return Ok(map);

        }

        //Verilen id ile vehicle ve container görüntülemek için kullanıldı.
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var result = await _unitOfWork.Vehicle.GetById(id);
            return Ok(result);
        }

        //Yeni vehicle eklemek için kullanıldı.
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] VehicleDto vehicledto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicledto);
            await _unitOfWork.Vehicle.Add(vehicle);
            var result = _unitOfWork.Complete();
            return Ok(result==1?"Added Succesfully.":"Not Added.");
        }

        //Vehicle bilgisi güncellemesi için kullanıldı.
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] VehicleDto vehicledto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicledto);
            await _unitOfWork.Vehicle.Update(vehicle);
            var result = _unitOfWork.Complete();
            return Ok(result == 1 ? "Updated Succesfully." : "Not Updated.");
        }

        //Vehicle ve container silinmesi için kullanıldı.
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _unitOfWork.Vehicle.Delete(id);
            var result = _unitOfWork.Complete();
            return Ok(result== 1 ? "Deleted Succesfully." : "Not Deleted.");
        }

        //Kümeleme işlemlerinin gösterimi için kullanıldı.
        [HttpPost("cluster")]
        public async Task<IActionResult> Cluster([FromBody] ClusterModel clusterModel)
        {
            var vehicle = await _unitOfWork.Vehicle.GetById(clusterModel.VehicleId);
            if (vehicle == null)
            {
                return BadRequest("Vehicle not found.");
            }
            if (vehicle.Containers.Count < clusterModel.N)
            {
                return BadRequest("N cannot be greater than the number of container.");
            }
            if (vehicle.Containers.Count % clusterModel.N !=0)
            {
                return BadRequest("Containers cannot be clustered evenly. Containers count: "+vehicle.Containers.Count);
            }

            var list = new List<IEnumerable<ContainerDto>>(); 
            var clusterCount = vehicle.Containers.Count/clusterModel.N; //küme sayısına erişmek için.
            for (int i = 0; i < clusterCount; i++)
            {
                //Örneğin N=3 => i=0, skip=0, take=3 || i=1, skip=3, take=3  || i=2, skip=6, take=3.... 
                var x = vehicle.Containers.ToList().Skip(i * clusterModel.N).Take(clusterModel.N);
                var xmap = _mapper.Map<IEnumerable<ContainerDto>>(x);
                list.Add(xmap);
            }          
            return Ok(list);
        }
    }
}
