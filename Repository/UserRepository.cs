using API.Contract.IRepository;
using API.Data;
using API.Data.Dto;
using API.Data.Models;

namespace API.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
     
        public UserRepository(AppDbContext appContext): base(appContext)
        {
            
        }

        // public string CreateNewRegistor(RegistorDto userDto)
        // {
        //     var user = new User
        //     {
        //         FirstName = userDto.FirstName,
        //         LastName = userDto.LastName,
        //         Email = userDto.Email,
        //         PhoneNumber = userDto.PhoneNumber,
        //         IsEmailVerifed = false, 
        //         IsMobileNumberVerified = false, 
        //         Password = userDto.Password,
        //         UserLockId = new UserLock{
        //             IsLocked = false
        //         }
        //     };

        //     _appContext.User.Add(user);
        //     _appContext.SaveChanges();

        //     return user.Email;
        // }

        // public bool CheckEmailExist(string email){
        //     var user = _appContext.User.FirstOrDefault(x => x.Email == email);
        //     return user != null;
        // }



    }
}