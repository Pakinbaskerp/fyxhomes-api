using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Models
{
    [Table("user")]
    public class User:Base
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }
        public bool IsMobileNumberVerified { get; set; }

        public Guid UserLockId { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        
    }
}