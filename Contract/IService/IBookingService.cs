
using API.Data.Dto;

namespace API.Contract.IService;
public interface IBookingService
{
    Guid CreateNewCarpenterBooking(BookCarpenterWithServiceDto bookCarpenterWithServiceDto, Guid UserId, Guid carpenterId);
    List<BookedDetailsDto> GetListOfBookings(Guid userId);
    List<ServiceDto> GetCartDetails(Guid UserId);
    void AddToCart(Guid userId, Guid serviceId);
}