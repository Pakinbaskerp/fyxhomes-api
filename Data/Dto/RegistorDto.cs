using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class RegisterDto
    {
        [DataMember(Name = "email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataMember(Name = "first_name")]
        [Required]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "password")]
        [Required]
        public string Password { get; set; }

        [DataMember(Name = "is_user")]
        public bool IsUser { get; set; }

        [DataMember(Name = "is_carpenter")]
        public bool IsCarpender { get; set; }
    }
}
