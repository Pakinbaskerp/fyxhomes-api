using System;
using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class ServiceDto
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "service_name")]
        public string? ServiceName { get; set; }

        [DataMember(Name = "service_description")]
        public string? ServiceDescription { get; set; }

        [DataMember(Name = "sort_order")]
        public int SortOrder { get; set; }

        [DataMember(Name = "file_name")]
        public string? FileName { get; set; }

        [DataMember(Name = "file_path")]
        public string? FilePath { get; set; }

        [DataMember(Name = "price")]
        public int Price { get; set; }

        [DataMember(Name = "currency")]
        public string? Currency { get; set; }
    }
}
