using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class GetCategoryListDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set;}
        public string CategoryDescription { get; set;}
        public string FilePath { get; set;}
        public int SortOrder { get; set; }
    }
}