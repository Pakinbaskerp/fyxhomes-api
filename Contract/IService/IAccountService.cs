using API.Data.Dto;

namespace API.Contract.IService
{
    public interface IAccountService
    {
        string CreateNewRegistor(RegisterDto user);
        bool CheckEmailExist(string email);

        Task<AuthResponseDto> LoginWithUser(LoginDto loginDto);
        Guid UpdateUserProfile(Guid userId,UpdateProfileDto updateProfileDto);



    }
}