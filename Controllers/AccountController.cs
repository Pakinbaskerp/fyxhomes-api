using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;
using API.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AccountController(IAccountService accountService, IRefreshTokenService refreshTokenService)
        {
            _accountService = accountService;
            _refreshTokenService = refreshTokenService;
        }


        [HttpPost]
        [Route("api/auth/register")]
        public IActionResult Register([FromBody] RegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isEmailExist = _accountService.CheckEmailExist(user.Email);
            if (isEmailExist)
            {
                return Conflict("Email already exists");
            }

            var result = _accountService.CreateNewRegistor(user);
            RegisterDto reff = new RegisterDto() { Email = result };
            return Ok(reff);
        }
        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> Login([FromBody][Required] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.LoginWithUser(loginDto);

            if (!result.IsSuccess)
            {
                return Unauthorized(result.message);
            }

            return Ok(result);
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPut]
        [Route("api/profile/update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
        {


            Guid userId = Guid.Parse(User.FindFirst("userId")?.Value);

            var user = _accountService.UpdateUserProfile(userId, updateProfileDto);


            return Ok(user);
        }

        [HttpPost]
        [Route("api/auth/refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenDto authResponseDto)
        {

            return Ok(_refreshTokenService.GenereateRefreshToken(authResponseDto));


        }

        [HttpPost]
        [Route("api/auth/logout")]
        public IActionResult Logout([FromBody] RefreshTokenDto authResponseDto)
        {
            _refreshTokenService.ExpireToken(Guid.Parse(authResponseDto.AccessToken));
            return Ok();
        }
    }
}