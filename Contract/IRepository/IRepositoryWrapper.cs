using Microsoft.EntityFrameworkCore.Storage;

namespace API.Contract.IRepository
{
    public interface IRepositoryWrapper
    {
        IAccountRepository User { get;}

        Task SaveAsync();

        
        void Save();

        
        IDbContextTransaction BeginTransaction();
    }
}