using API.Data.Dto;

namespace API.Contract.IService
{
    public interface IAccountService
    {
        string CreateNewRegistor(RegistorDto user);
        bool CheckEmailExist(string email);

        Task<AuthResponseDto> LoginWithUser(LoginDto loginDto);

    }
}