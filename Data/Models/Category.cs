namespace API.Data.Models
{
    public class Category : Base
    {
        public string CategoryName { get; set;}
        public Guid AssetId { get; set;}
        public int SortOrder { get; set;}
        public string Description {get; set;}
        
    }
}