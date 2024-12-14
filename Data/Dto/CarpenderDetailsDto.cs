using System;
using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class CarpenterDetailsDto
    {
        [DataMember(Name = "carpenter_id")]
        public Guid CarpenterId { get; set; }

        [DataMember(Name = "carpenter_name")]
        public string? CarpenterName { get; set; }
    }
}
