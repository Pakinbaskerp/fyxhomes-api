using API.Contract.IRepository;
using API.Data;
using API.Data.Dto;
using API.Data.Models;

namespace API.Repository
{
    public class RepositoryCart : RepositoryBase<Cart>, IRepositoryCart
    {
     
        public RepositoryCart(AppDbContext appContext): base(appContext)
        {
            
        }
    }
}