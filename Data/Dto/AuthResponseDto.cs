using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class AuthResponseDto
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "is_success")]
        public bool IsSuccess { get; set; }
        [DataMember(Name = "message")]
        public string message { get; set; }
    }
}