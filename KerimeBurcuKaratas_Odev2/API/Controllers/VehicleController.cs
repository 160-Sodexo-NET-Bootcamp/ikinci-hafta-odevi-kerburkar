using Data.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VehicleController> _logger;
        public VehicleController(IUnitOfWork unitOfWork,ILogger<VehicleController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        
        //Tüm araç listesini çekmek için kullanıldı.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.Vehicle.GetAll();
            return Ok(result);  
        }
    }
}
