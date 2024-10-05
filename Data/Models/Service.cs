namespace API.Data.Models
{
    public class Services : Base
    {
        public string ServiceName { get; set;}
        public Guid AssetId { get; set;}
        public int SortOrder { get; set;}
        public Guid CategoryId {get; set;}
        public Guid PriceId { get; set;}
        public string Description { get; set;}

    }
}