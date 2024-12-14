using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;

namespace API.Service
{
    public class CarpenterService : ICarpenterService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public CarpenterService(IRepositoryWrapper repositoryWrapper){
            _repoWrapper = repositoryWrapper;
        }
        public List<CarpenterDetailsDto> GetCarpenterDetails()
        {
            List<CarpenterDetailsDto> carpenters =(from u in _repoWrapper.User.FindByCondition(x=> x.IsActive && x.Role.Equals("Carpenter"))
            select new CarpenterDetailsDto{
            CarpenterId = u.Id,
            CarpenterName = u.FirstName +" "+ u.LastName,
            }).ToList();

            return carpenters;

        }
        public List<BookedDetailsDto> GetListOfBookings(Guid UserId){
            List<BookedDetailsDto> booked = (from b in _repoWrapper.BookingDetail.FindByCondition(x=> x.IsActive && x.CarpenterId.Equals(UserId))
            join u in _repoWrapper.User.FindByCondition(x=> x.IsActive ) on b.CarpenterId equals u.Id
            join s in _repoWrapper.Services.FindByCondition(x=> x.IsActive) on b.ServiceId equals s.Id
            join p in _repoWrapper.PriceCatalogue.FindByCondition(x=> x.IsActive) on s.PriceId equals p.Id
            select new BookedDetailsDto
            {
                ServiceName = s.ServiceName,
                ServiceDescription = s.Description,
                Price = p.Price,
                BookedDateTime = b.BookedDate,
                BookedCarpenterName = u.FirstName +" "+u.LastName,
            }).ToList();

            return booked;
        }
    }
}