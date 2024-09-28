using System.ComponentModel.DataAnnotations;

namespace API.Data.Dto
{
    public class RegistorDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        // public List<string> Roles { get; set;}

        



    }
}