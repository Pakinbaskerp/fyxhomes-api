using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class CategoryDto
    {
        [DataMember(Name = "category_name")]
        public string? CategoryName { get; set; }

        [DataMember(Name = "category_description")]
        public string? CategoryDescription { get; set; }

        [DataMember(Name = "sort_order")]
        public int SortOrder { get; set; }

        [DataMember(Name = "file_name")]
        public string? FileName { get; set; }

        [DataMember(Name = "file_path")]
        public string? FilePath { get; set; }
    }
}
