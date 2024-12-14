using System.Runtime.Serialization;

namespace API.Data.Dto;

[DataContract]
public class BookCarpenterWithServiceDto
{
    [DataMember(Name = "service_id")]
    public Guid ServiceId { get; set; }
    [DataMember(Name = "booked_date")]
    public DateTime BookedDate { get; set; }
}