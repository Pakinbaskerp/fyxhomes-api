using API.Contract.IRepository;
using API.Data;
using API.Data.Dto;
using API.Data.Models;

namespace API.Repository
{
    public class RepositoryPriceCatalogue : RepositoryBase<PriceCatalogue>, IRepositoryPriceCatalogue
    {
     
        public RepositoryPriceCatalogue(AppDbContext appContext): base(appContext)
        {
            
        }
    }
}