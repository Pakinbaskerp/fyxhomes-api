using API.Contract.IRepository;
using API.Data;
using API.Data.Dto;
using API.Data.Models;

namespace API.Repository
{
    public class RepositoryCategory : RepositoryBase<Category>, IRepositoryCategory
    {
     
        public RepositoryCategory(AppDbContext appContext): base(appContext)
        {
            
        }
    }
}