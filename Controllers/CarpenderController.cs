using API.Contract.IService;
using API.Data.Dto;
using API.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarpenterController : ControllerBase
    {
        private readonly ICarpenterService _carpenterService;
        public CarpenterController(ICarpenterService carpenterService){
            _carpenterService = carpenterService;
        }

        [HttpGet]
        [Route("api/carpenter/list")]
        public IActionResult GetCarpenterList(){
            List<CarpenterDetailsDto> carpenters = _carpenterService.GetCarpenterDetails();
            return Ok(carpenters);
        }
        [Authorize(Roles = "Carpenter")]
        [HttpGet]
        [Route("api/carpenter/booking")]
        public IActionResult GetCarpenterBookings(){
            Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok(_carpenterService.GetListOfBookings(userId));
        } 

        
    }
}