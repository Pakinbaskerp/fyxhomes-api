namespace API.Contract.IService
{
    public interface IPasswordHasherService
    {
        (string hashedPassword, string salt) HashPassword(string password);
        bool VerifyPassword(string password, string salt, string hashedPassword);
    }
}
