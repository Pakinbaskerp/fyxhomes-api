using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class EmailExtractorDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}