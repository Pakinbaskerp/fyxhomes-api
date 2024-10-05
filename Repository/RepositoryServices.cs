using API.Contract.IRepository;
using API.Data;
using API.Data.Dto;
using API.Data.Models;

namespace API.Repository
{
    public class RepositoryServices : RepositoryBase<Services>, IRepositoryServices
    {
     
        public RepositoryServices(AppDbContext appContext): base(appContext)
        {
            
        }
    }
}