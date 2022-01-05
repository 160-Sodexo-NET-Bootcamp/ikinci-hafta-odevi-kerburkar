using API.Common;
using API.Dtos;
using API.Model;
using AutoMapper;
using Data.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClusterController> _logger;
        private readonly IMapper _mapper;
        public ClusterController(IUnitOfWork unitOfWork, ILogger<ClusterController> logger, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Mesafeye göre kümeleme işlemlerinin gösterimi için kullanıldı.
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
            if (vehicle.Containers.Count % clusterModel.N != 0)
            {
                return BadRequest("Containers cannot be clustered evenly. Containers count: " + vehicle.Containers.Count);
            }

           
            //Gelen container listesi ContainerDistance Map edildi. Hesaplanan distance değerlerini çekmek için kullanıldı.
            var containerWithDistance = _mapper.Map<IEnumerable<ContainerDistanceModel>>(vehicle.Containers);
            
            //Her containerin merkeze(0,0) olan uzaklığı hesaplatıldı.
            foreach (var container in containerWithDistance)
            {
                ////1.yol olarak GeoCoordinate kütüphanesini kullanıldığında kullanılacak kod. 
                //container.Distance = GeoCoordinateHelper.CalculateDistance(container); 

                //2.yol olarak da kütüphane kullanmadan, her container mesafesini, başlangıç noktasına(0,0) olan uzaklık hesaplatıldı.
                container.Distance = DistanceAlgorithm.DistanceBetweenPlaces(0,0, Decimal.ToDouble(container.Longitude), Decimal.ToDouble(container.Latitude));
            }
          
            //Mesafeye göre sıralandı. Başlangıç noktasına en yakından en uzağa göre sıralandı.
            var newOrderedContainerList = containerWithDistance.OrderBy(x => x.Distance).ToList();  

            var list = new List<IEnumerable<ContainerDto>>();
            var clusterCount = newOrderedContainerList.Count / clusterModel.N; //küme sayısına erişmek için.
            for (int i = 0; i < clusterCount; i++)
            {
                //Örneğin N=3 => i=0, skip=0, take=3 || i=1, skip=3, take=3  || i=2, skip=6, take=3.... 
                var x = newOrderedContainerList.Skip(i * clusterModel.N).Take(clusterModel.N);
                var xmap = _mapper.Map<IEnumerable<ContainerDto>>(x);
                list.Add(xmap);
            }
            return Ok(list);
        }


    }
}
