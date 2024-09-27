using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;
using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

         private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistorDto user)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            bool isEmailExist = _accountService.CheckEmailExist(user.Email); 
            if(isEmailExist){
                return Conflict(isEmailExist);
            }
            var result = _accountService.CreateNewRegistor(user);
            return Ok(result);
        }
    }
}
