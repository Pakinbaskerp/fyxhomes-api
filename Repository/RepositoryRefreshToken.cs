using API.Contract.IRepository;
using API.Data;
using Entities.Models;

namespace API.Repository
{
    public class RepositoryRefreshToken : RepositoryBase<RefreshToken>, IRepositoryRefreshToken
    {
     
        public RepositoryRefreshToken(AppDbContext appContext): base(appContext)
        {
            
        }
    }
}