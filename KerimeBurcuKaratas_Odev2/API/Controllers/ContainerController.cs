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
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContainerController> _logger;
        private readonly IMapper _mapper;

        public ContainerController(IUnitOfWork unitOfWork, ILogger<ContainerController> logger, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        //Tüm container listesini göstermek için kullanıldı.
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.Container.GetAll();
            var map = _mapper.Map<IEnumerable<ContainerDto>>(result);
            return Ok(map);
        }

        //Yeni container eklemek için kullanıldı.
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ContainerDto containerdto)
        {
            var container = _mapper.Map<Container>(containerdto);
            await _unitOfWork.Container.Add(container);
            var result = _unitOfWork.Complete();
            return Ok(result== 1 ? "Added Succesfully." : "Not Added.");
        }

        //Container bilgisi güncellemesi için kullanıldı.
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ContainerDto containerdto)
        {
            var entity = await _unitOfWork.Container.GetById(containerdto.Id);
            if (entity.VehicleId != containerdto.VehicleId)
            {
                return BadRequest("VehicleId can not different from before!");

            }
            var container = _mapper.Map<Container>(containerdto);
            await _unitOfWork.Container.Update(container);
            var result = _unitOfWork.Complete();
            return Ok(result == 1 ? "Updated Succesfully." : "Not Updated.");
        }

        //Container silinmesi için kullanıldı.
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _unitOfWork.Container.Delete(id);
            var result = _unitOfWork.Complete();
            return Ok(result == 1 ? "Deleted Succesfully." : "Not Deleted.");
        }
    }
}
