using System.Runtime.Serialization;

namespace API.Data.Dto;

[DataContract]
public class CartDetailDto{
    [DataMember(Name ="services")]
    public List<ServiceDto> services{get; set;}

}