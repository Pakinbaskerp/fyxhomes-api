using API.Contract.IRepository;
using API.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _appContext; // Use your DbContext (AppDbContext)
        private IAccountRepository _User;

        public RepositoryWrapper(AppDbContext appContext) // Change to AppDbContext
        {
            _appContext = appContext;
        }

        public IAccountRepository User
        {
            get
            {
                if (_User == null)
                {
                    _User = new AccountRepository(_appContext); // Pass the correct context
                }
                return _User;
            }
        }

        // public IAccountRepository User => throw new NotImplementedException();


        public void Save()
        {
            _appContext.OnBeforeSaving(GetCurrentUser()); // Ensure OnBeforeSaving is in AppDbContext
            _appContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            _appContext.OnBeforeSaving(GetCurrentUser()); // Ensure OnBeforeSaving is in AppDbContext
            _ = await _appContext.SaveChangesAsync();
        }

        // Uncomment and implement your GetCurrentUser method, or pass it as needed
        private Guid GetCurrentUser()
        {
            return Guid.Parse("b13c9816-0c51-4928-80ea-2b94e1ec7982");
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _appContext.Database.BeginTransaction();
        }
    }
}
