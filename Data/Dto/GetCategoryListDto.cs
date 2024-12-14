using System;
using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class GetCategoryListDto
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "category_name")]
        public string? CategoryName { get; set; }

        [DataMember(Name = "category_description")]
        public string? CategoryDescription { get; set; }

        [DataMember(Name = "file_path")]
        public string? FilePath { get; set; }

        [DataMember(Name = "sort_order")]
        public int SortOrder { get; set; }
    }
}
