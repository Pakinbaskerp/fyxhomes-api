using API.Contract.IRepository;
using API.Data;
using API.Data.Dto;
using API.Data.Models;

namespace API.Repository
{
    public class RepositoryBookingDetail : RepositoryBase<BookingDetail>, IRepositoryBookingDetail
    {
     
        public RepositoryBookingDetail(AppDbContext appContext): base(appContext)
        {
            
        }
    }
}