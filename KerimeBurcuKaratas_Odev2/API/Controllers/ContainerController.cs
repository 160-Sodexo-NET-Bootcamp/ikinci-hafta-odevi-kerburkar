using Data.DataModel;
using Data.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContainerController> _logger;
        public ContainerController(IUnitOfWork unitOfWork, ILogger<ContainerController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }

        //Tüm container listesini göstermek için kullanıldı.
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.Container.GetAll();
            return Ok(result);
        }

        //Yeni container eklemek için kullanıldı.
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Container container)
        {
            await _unitOfWork.Container.Add(container);
            var result = _unitOfWork.Complete();
            return Ok(result);
        }

        //Container bilgisi güncellemesi için kullanıldı.
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Container container)
        {
            await _unitOfWork.Container.Update(container);
            var result = _unitOfWork.Complete();
            return Ok(result);
        }

        //Container silinmesi için kullanıldı.
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            await _unitOfWork.Container.Delete(id);
            var result = _unitOfWork.Complete();
            return Ok(result);
        }
    }
}
