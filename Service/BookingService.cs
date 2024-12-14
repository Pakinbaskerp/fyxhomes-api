
using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;
using API.Data.Models;

namespace API.Service;
public class BookingService : IBookingService
{
     private readonly IRepositoryWrapper _repoWrapper;
     public BookingService(IRepositoryWrapper repositoryWrapper){
        _repoWrapper = repositoryWrapper;
     }
    public Guid CreateNewCarpenterBooking(BookCarpenterWithServiceDto bookCarpenterWithServiceDto,Guid userId,Guid carpenterId)
    {
        BookingDetail bookingDetail= new BookingDetail{
            Id = Guid.NewGuid(),
            UserId = userId,
            CarpenterId = carpenterId,
            ServiceId = bookCarpenterWithServiceDto.ServiceId,
            BookedDate = bookCarpenterWithServiceDto.BookedDate,
        };
        _repoWrapper.BookingDetail.Create(bookingDetail);
        _repoWrapper.Save();
        return bookingDetail.Id;
    }
    
    public List<BookedDetailsDto> GetListOfBookings(Guid userId){
        List<BookedDetailsDto> bookedDetails = (from b in _repoWrapper.BookingDetail.FindByCondition(x=> x.IsActive && x.UserId.Equals(userId))
        join s in _repoWrapper.Services.FindByCondition(x=> x.IsActive ) on b.ServiceId equals s.Id
        join p in _repoWrapper.PriceCatalogue.FindByCondition(x=> x.IsActive) on s.PriceId equals p.Id
        join u in _repoWrapper.User.FindByCondition(x=> x.IsActive ) on b.CarpenterId equals u.Id
        select new BookedDetailsDto{
            ServiceName = s.ServiceName,
            ServiceDescription = s.Description,
            Price = p.Price,
            BookedDateTime = b.BookedDate,
            BookedCarpenterName = u.FirstName + " " + u.LastName,
        }).ToList();

        return bookedDetails;
    }
    public void AddToCart(Guid userId, Guid serviceId)
    {
        var addToCart = new Cart{
            ServiceId = serviceId,
            UserId = userId
        };
        _repoWrapper.Cart.Create(addToCart);
        _repoWrapper.Save();
    }
    public List<ServiceDto> GetCartDetails(Guid UserId){
        var serviceDetails = _repoWrapper.Cart.FindByCondition(x => x.IsActive && x.UserId.Equals(UserId)).Select(x=> x.ServiceId).ToList();
        return (from service in  _repoWrapper.Services.FindByCondition(x => x.IsActive && serviceDetails.Contains(x.Id))
                    join asset in _repoWrapper.Asset.FindByCondition(x=> x.IsActive ) on service.AssetId equals asset.Id
                    join price in _repoWrapper.PriceCatalogue.FindByCondition(x => x.IsActive) on service.PriceId equals price.Id
                    select new ServiceDto
                    {
                        Id = service.Id,
                        ServiceName = service.ServiceName,
                        ServiceDescription = service.Description,
                        SortOrder = service.SortOrder,
                        FileName = asset.FileName,
                        FilePath = asset.FilePath,
                        Price = price.Price,
                        Currency = price.Currency
                    })
                    .ToList();
    }
}