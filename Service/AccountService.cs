using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;
using API.Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Service
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IConfiguration _configuration;

        public AccountService(IRepositoryWrapper repoWrapper, IPasswordHasherService passwordHasherService, IConfiguration configuration)
        {
            _repoWrapper = repoWrapper;
            _passwordHasherService = passwordHasherService;
            _configuration = configuration;
        }

        public bool CheckEmailExist(string email)
        {
            return _repoWrapper.User.FindByCondition(x => x.Email == email).Any();
        }

        public string CreateNewRegistor(RegistorDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Invalid email address");
            }

            var (hashedPassword, salt) = _passwordHasherService.HashPassword(user.Password);


            User newUser = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Password = hashedPassword,
                Salt = salt
            };

            _repoWrapper.User.Create(newUser);
            _repoWrapper.Save();

            return newUser.Email;

        }




        public async Task<AuthResponseDto> LoginWithUser(LoginDto loginDto)
        {
            var user = _repoWrapper.User.FindFirstByCondition(x => x.Email.Equals(loginDto.Email));
            if (user is null || !_passwordHasherService.VerifyPassword(loginDto.Password, user.Salt, user.Password))
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    message = "User is not valid"
                };
            }
            string token = GenerateToken(loginDto.Email);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                message = "Login successful"
            };
        }

        public string GenerateToken(string userId)
        {
            User user = _repoWrapper.User.FindFirstByCondition(x => x.Email == userId);

            var issuer = _configuration["JWTSetting:validIssuer"];
            var audience = _configuration["JWTSetting:validAudience"];
            var key = Encoding.UTF8.GetBytes(_configuration["JWTSetting:securityKey"]);
            var signingCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha512Signature
                                );

            var subject = new ClaimsIdentity(new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            });
            var expires = DateTime.UtcNow.AddMinutes(10);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;

        }



    }
}