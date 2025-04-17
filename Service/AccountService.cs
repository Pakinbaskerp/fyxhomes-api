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
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IConfiguration _configuration;

        public AccountService(IRepositoryWrapper repoWrapper, IPasswordHasherService passwordHasherService, IConfiguration configuration, IRefreshTokenService refreshTokenService)
        {
            _repoWrapper = repoWrapper;
            _passwordHasherService = passwordHasherService;
            _configuration = configuration;
            _refreshTokenService = refreshTokenService;
        }

        public bool CheckEmailExist(string email)
        {
            return _repoWrapper.User.FindByCondition(x => x.Email == email).Any();
        }

        public string CreateNewRegistor(RegisterDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || !Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Invalid email address");
            }

            var (hashedPassword, salt) = _passwordHasherService.HashPassword(user.Password);


            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Password = hashedPassword,
                Salt = salt
            };
            if (user.IsUser)
            {
                newUser.Role = "User";
            }
            else if (user.IsCarpender)
            {
                newUser.Role = "Carpenter";
            }
            else
            {
                newUser.Role = "Admin";
            }


            UserLock userLock = new UserLock
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                FailedLogInCount = 0,
                IsLocked = false
            };
            newUser.UserLockId = userLock.Id;
            _repoWrapper.User.Create(newUser);
            _repoWrapper.UserLock.Create(userLock);

            _repoWrapper.Save();

            return newUser.Email;

        }




        public async Task<AuthResponseDto> LoginWithUser(LoginDto loginDto)
        {
            User user = _repoWrapper.User.FindFirstByCondition(x => x.Email.Equals(loginDto.Email));
            if (user is null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    message = "User does not exist"
                };
            }

            UserLock userLock = _repoWrapper.UserLock.FindFirstByCondition(x => x.IsActive && x.UserId.Equals(user.Id));
            if (userLock.IsLocked)
            {

                return new AuthResponseDto
                {
                    IsSuccess = false,
                    message = "User account is locked"
                };
            }

            if (!_passwordHasherService.VerifyPassword(loginDto.Password, user.Salt, user.Password))
            {
                int userLockCount = userLock.FailedLogInCount + 1;
                userLock.FailedLogInCount = userLockCount;
                if (userLock.FailedLogInCount > 5)
                {
                    userLock.IsLocked = true;
                }

                _repoWrapper.UserLock.Update(userLock);
                _repoWrapper.Save();

                return new AuthResponseDto
                {
                    IsSuccess = false,
                    message = "Invalid password"
                };
            }

            string token = GenerateToken(loginDto.Email);
            string accessToken = GenerateRefreshToken(user.Id, token).ToString();


            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                AccessToken = accessToken,
                message = "Login successful"
            };
        }

        public Guid UpdateUserProfile(Guid userId, UpdateProfileDto updateProfileDto)
        {
            User user = _repoWrapper.User.FindByCondition(x => x.IsActive && x.Id.Equals(userId)).FirstOrDefault();
            if (user != null)
            {
                if (updateProfileDto.Email != null)
                {
                    user.Email = updateProfileDto.Email;
                }
                if (updateProfileDto.FirstName != null)
                {
                    user.FirstName = updateProfileDto.FirstName;
                }
                if (updateProfileDto.LastName != null)
                {
                    user.LastName = updateProfileDto.LastName;

                }


            }
            _repoWrapper.User.Update(user);
            _repoWrapper.Save();
            return user.Id;
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
                            new Claim("userId", user.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.Role),
                            });
            var expires = DateTime.UtcNow.AddMinutes(100);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;

        }
        public Guid GenerateRefreshToken(Guid userId, string token)
        {
            Guid accessToken = Guid.NewGuid();
            _refreshTokenService.Create(userId, token, accessToken);
            return accessToken;
        }



    }
}