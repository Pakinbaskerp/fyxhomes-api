using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;
using API.Data.Models;

namespace API.Service
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public AccountService(IRepositoryWrapper repoWrapper){
            _repoWrapper = repoWrapper;
        }

        public bool CheckEmailExist(string email)
        {
            return _repoWrapper.User.FindByCondition(x=> x.Email == email).Any();
        }

        public string CreateNewRegistor(RegistorDto user)
        {
            User newUser = new User{
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                };

                 _repoWrapper.User.Create(newUser);
                 _repoWrapper.Save();

                 return newUser.Email;

        }

    }
}