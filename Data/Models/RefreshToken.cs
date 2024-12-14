
namespace Entities.Models;
public class RefreshToken 
{
    public Guid Id{ get; set; }
    public Guid UserId { get; set; }
    public Guid AccessToken { get; set; }
    public string Token { get; set; }
    public DateTime ExpireTime { get; set; }
    public bool IsRevoked { get; set; }
}