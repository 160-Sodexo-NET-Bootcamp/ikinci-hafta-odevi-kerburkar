using API.Dtos;
using AutoMapper;
using Data.DataModel;
using Data.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        public async Task<IActionResult> Add([FromBody] Vehicle vehicle)
        {
            await _unitOfWork.Vehicle.Add(vehicle);
            var result = _unitOfWork.Complete();
            return Ok(result);
        }

        //Vehicle bilgisi güncellemesi için kullanıldı.
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Vehicle vehicle)
        {
            await _unitOfWork.Vehicle.Update(vehicle);
            var result = _unitOfWork.Complete();
            return Ok(result);
        }

        //Vehicle ve container silinmesi için kullanıldı.
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _unitOfWork.Vehicle.Delete(id);
            var result = _unitOfWork.Complete();
            return Ok(result);
        }
    }
}
