using Microsoft.EntityFrameworkCore.Storage;

namespace API.Contract.IRepository
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get;}
        IUserLockRepository UserLock {get;}
         IRepositoryAsset Asset  {get;}
        IRepositoryCategory Category  {get;}
        IRepositoryPriceCatalogue PriceCatalogue {get;}
        IRepositoryServices Services {get;}
        IRepositoryBookingDetail BookingDetail {get;}
        IRepositoryRefreshToken RefreshToken {get;}
        IRepositoryCart Cart {get;}

        Task SaveAsync();

        
        void Save();

        
        IDbContextTransaction BeginTransaction();
    }
}