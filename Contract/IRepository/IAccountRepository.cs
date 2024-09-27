using API.Data.Dto;
using API.Data.Models;

namespace API.Contract.IRepository
{
    public interface IAccountRepository : IRepositoryBase<User>
     
    {
        // string CreateNewRegistor(RegistorDto user);
        // bool CheckEmailExist(string email);

    }
}
