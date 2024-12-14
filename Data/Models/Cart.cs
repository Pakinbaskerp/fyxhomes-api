namespace API.Data.Models
{
    public class Cart : Base
    {
        public Guid ServiceId { get; set;}
        public Guid UserId {get; set;}
        
    }
}