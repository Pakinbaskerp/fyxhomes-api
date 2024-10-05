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
        private IUserRepository _User;
        private IUserLockRepository _UserLock;
        public IRepositoryAsset _Asset;
        public IRepositoryCategory _Category;
        public IRepositoryPriceCatalogue _PriceCatalogue;
        public IRepositoryServices _Services;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public RepositoryWrapper(AppDbContext appContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) // Change to AppDbContext
        {
            _appContext = appContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public IUserRepository User
        {
            get
            {
                if (_User == null)
                {
                    _User = new UserRepository(_appContext); // Pass the correct context
                }
                return _User;
            }
        }

        public IUserLockRepository UserLock
        {
            get
            {
                if (_UserLock == null)
                {
                    _UserLock = new UserLockRepository(_appContext);
                }
                return _UserLock;

            }
        }
        public IRepositoryServices Services
        {
            get
            {
                if (_Services == null)
                {
                    _Services = new RepositoryServices(_appContext);
                }
                return _Services;

            }
        }
        public IRepositoryAsset Asset
        {
            get
            {
                if (_Asset == null)
                {
                    _Asset = new RepositoryAsset(_appContext);
                }
                return _Asset;

            }
        }
        public IRepositoryPriceCatalogue PriceCatalogue
        {
            get
            {
                if (_PriceCatalogue == null)
                {
                    _PriceCatalogue = new RepositoryPriceCatalogue(_appContext);
                }
                return _PriceCatalogue;

            }
        }
        public IRepositoryCategory Category
        {
            get
            {
                if (_Category == null)
                {
                    _Category = new RepositoryCategory(_appContext);
                }
                return _Category;

            }
        }



        public void Save()
        {
            _appContext.OnBeforeSaving(GetCurrentUser()); 
            _appContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            _appContext.OnBeforeSaving(GetCurrentUser()); 
            _ = await _appContext.SaveChangesAsync();
        }

        private Guid GetCurrentUser()
        {
            return GetPersonIdentity();
        }

        public Guid GetPersonIdentity()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                return GetPersonIdFromJwtToken(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            }
            return Guid.Empty;
        }

        /// <summary>
        /// The method <c>GetPersonIdFromJwtToken</c> to get person ID from the jwt token.
        /// </summary>
        /// <param name="token"></param>
        public System.Guid GetPersonIdFromJwtToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                token = token.Replace("Bearer ", string.Empty);
                var key = _configuration["JWTSetting:securityKey"];
                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception("JWT security key is missing in configuration.");
                }
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSetting:securityKey"])),
                    ValidateLifetime = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken == null)
                    throw new SecurityTokenException("Invalid token");

                // Log the algorithm used in the token header
                var algorithm = jwtSecurityToken.Header["alg"];
                Console.WriteLine($"Algorithm from token header: {algorithm}");

                //if (!algorithm.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                //    throw new SecurityTokenException("Invalid token");

                var identity = principal?.Identities?.FirstOrDefault();

                if (identity == null)
                {
                    throw new NullReferenceException("Identity not found in token.");
                }

                // Use "userId" claim instead of "nameid"
                var userIdClaim = identity.FindFirst("userId");

                if (userIdClaim == null)
                {
                    throw new NullReferenceException("'userId' claim not found in token.");
                }

                var personId = System.Guid.Parse(userIdClaim.Value);
                return personId;
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
