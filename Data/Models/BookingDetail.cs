using API.Data.Models;

public class BookingDetail : Base{
    public Guid ServiceId { get; set;}
    public Guid UserId { get; set;}
    public Guid CarpenterId { get; set;}
    public DateTime BookedDate { get; set;}
}