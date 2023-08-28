using EmailTrigger.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmaillTrigger.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService; 
        }

        [HttpGet("sellers")]
        public IActionResult Get() 
        {
            //var result = _sellerService.GetData();
            _sellerService.SendSellerEmail();
            return Ok();
        }
    }
}
