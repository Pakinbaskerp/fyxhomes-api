using API.Contract.IRepository;
using API.Data;
using API.Data.Dto;
using API.Data.Models;

namespace API.Repository
{
    public class RepositoryAsset : RepositoryBase<Asset>, IRepositoryAsset
    {
     
        public RepositoryAsset(AppDbContext appContext): base(appContext)
        {
            
        }
    }
}