using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class UpdateProfileDto
    {
        [DataMember(Name = "email")]
        public string? Email { get; set; }

        [DataMember(Name = "first_name")]
        public string? FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string? LastName { get; set; }
    }
}
