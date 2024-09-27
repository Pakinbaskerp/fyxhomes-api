using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Models
{
    [Table("user_lock")]
    public class UserLock : Base
    {
        
        public Guid UserId { get; set; }

        public int FailedLogInCount { get; set; }

        public bool IsLocked { get; set; } = false;
    }
}