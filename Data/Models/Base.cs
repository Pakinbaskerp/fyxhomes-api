using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{

    public class Base
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid UpdatedBy { get; set; }

        public bool IsActive { get; set; }
    }
}