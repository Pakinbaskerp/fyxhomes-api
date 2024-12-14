
using API.Contract.IService;
using API.Data.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("api/book-service")]
        public IActionResult CreateNewBookingWithCarpenter([FromBody] BookCarpenterWithServiceDto bookCarpenterWithServiceDto, [FromQuery(Name = "carpenter-id")] Guid carpenterId)
        {
              Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);

              return Ok(_bookingService.CreateNewCarpenterBooking(bookCarpenterWithServiceDto, userId, carpenterId));
        }

        [HttpGet]
        [Route("api/book")]
        public IActionResult GetListOfBooking(){
            Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok(_bookingService.GetListOfBookings(userId));
        }

        [HttpPost]
        [Route("api/add-to-cart/{service-id}")]
        public IActionResult AddServiceToCart([FromRoute(Name = "service-id")] Guid serviceId){
            Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            _bookingService.AddToCart(userId, serviceId);
            return Ok();
        }
        [HttpGet]
        [Route("api/add-to-cart")]
        public IActionResult GetServiceToCartList(){
            Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok(_bookingService.GetCartDetails(userId));
        }



    }

}
