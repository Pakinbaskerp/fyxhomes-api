using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class UpdateProfileDto
    {
        public string? Email { get; set;}
        public string? FirstName{get;set;}
        public string? LastName{ get; set;}

    }
}