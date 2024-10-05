using API.Contract.IRepository;
using API.Data;
using API.Data.Models;

namespace API.Repository
{
    public class UserLockRepository : RepositoryBase<UserLock>, IUserLockRepository
    {
        public UserLockRepository(AppDbContext appContext): base(appContext)
        {

        }
        
    }
}