using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Models
{
    [Table("user")]
    public class User:Base
    {
        
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int PhoneNumber { get; set;}
        public string Email {get; set;}
        public bool IsEmailVerifed { get; set;}
        public bool IsMobileNumberVerified { get; set;}

        public UserLock UserLockId {get; set;}
        public string Password {get; set;}
        
    }
}