using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class LoginDto
    {
        [DataMember(Name = "email")]
        [Required]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        [Required]
        public string Password { get; set; }
    }
}
