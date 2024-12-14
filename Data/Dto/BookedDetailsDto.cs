using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class BookedDetailsDto
    {
        [DataMember(Name = "service_name")]
        public string? ServiceName { get; set; }

        [DataMember(Name = "service_description")]
        public string? ServiceDescription { get; set; } // Fixed typo

        [DataMember(Name = "price")]
        public int Price { get; set; }

        [DataMember(Name = "booked_date_time")]
        public DateTime BookedDateTime { get; set; }

        [DataMember(Name = "booked_carpenter_name")]
        public string? BookedCarpenterName { get; set; } // Fixed spelling of "Carpenter"
    }
}
