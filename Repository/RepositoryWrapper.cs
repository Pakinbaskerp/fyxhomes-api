using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Contract.IRepository;
using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace API.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _appContext; // Use your DbContext (AppDbContext)
        private IAccountRepository _User;
         private readonly IHttpContextAccessor _httpContextAccessor;
         private readonly IConfiguration _configuration;

        public RepositoryWrapper(AppDbContext appContext, IHttpContextAccessor httpContextAccessor) // Change to AppDbContext
        {
            _appContext = appContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IAccountRepository User
        {
            get
            {
                if (_User == null)
                {
                    _User = new AccountRepository(_appContext); // Pass the correct context
                }
                return _User;
            }
        }

        // public IAccountRepository User => throw new NotImplementedException();


        public void Save()
        {
            _appContext.OnBeforeSaving(GetCurrentUser()); // Ensure OnBeforeSaving is in AppDbContext
            _appContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            _appContext.OnBeforeSaving(GetCurrentUser()); // Ensure OnBeforeSaving is in AppDbContext
            _ = await _appContext.SaveChangesAsync();
        }

        // Uncomment and implement your GetCurrentUser method, or pass it as needed
         private Guid GetCurrentUser()
        {
            return GetPersonIdentity();
        }

        public Guid GetPersonIdentity()
        {
            if(_httpContextAccessor.HttpContext != null)
            {
                return GetPersonIdFromJwtToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            }
            return Guid.Empty;
        }

        /// <summary>
        /// The method <c>GetPersonIdFromJwtToken</c> to get person ID from the jwt token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public System.Guid GetPersonIdFromJwtToken(string token)
        {
            if (token != null)
            {
                token = token.Replace("Bearer ", string.Empty);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"])),
                    ValidateLifetime = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                var identity = principal.Identities.FirstOrDefault();
                var _personId = System.Guid.Parse(identity.FindFirst("nameid").Value);
                return _personId;
            }
            else
            {
                return Guid.Parse("d0e96ca8-7a2f-41d0-84b8-0c0298208def");
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _appContext.Database.BeginTransaction();
        }
    }
}
