using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class EmailMessageDto
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}