using System.Runtime.Serialization;

namespace API.Data.Dto
{
    [DataContract]
    public class GetAllServiceListDto
    {
        [DataMember(Name = "category")]
        public GetCategoryListDto Category { get; set; }

        [DataMember(Name = "services")]
        public List<ServiceDto> Services { get; set; }
    }
}
