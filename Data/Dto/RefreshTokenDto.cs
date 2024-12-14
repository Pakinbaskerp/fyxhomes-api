using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class RefreshTokenDto
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
