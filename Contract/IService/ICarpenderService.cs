using API.Data.Dto;

namespace API.Contract.IService
{
    public interface ICarpenterService
    {
         List<CarpenterDetailsDto> GetCarpenterDetails();
         List<BookedDetailsDto> GetListOfBookings(Guid UserId);
    }
}