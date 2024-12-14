using API.Contract.IService;
using API.Data.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/product/category")]
        public IActionResult CreateNewCategory([FromBody] CategoryDto categoryDto)
        {
            //   Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok(_productService.CreateNewCategory(categoryDto));

        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route("api/product/category")]
        public IActionResult GetCategoryList()
        {
            //   Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok(_productService.GetCategoryList());

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/product/category/service")]
        public IActionResult CreateNewService([FromBody] ServiceDto serviceDto, [FromQuery(Name = "category-id")] Guid categoryId){
            return Ok(_productService.CreateService(serviceDto,categoryId));
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route("api/product/category/service")]
        public IActionResult GetListOfServiceByCategory([FromQuery (Name = "category-id")]Guid categoryId){
            return Ok(_productService.GetListOfServicesByCategory(categoryId));
        }

        [Authorize(Roles ="Admin, User")]
        [HttpGet]
        [Route("api/product/categoryId/get-all")]
        public IActionResult GetListOfAllService(){
            return Ok(_productService.GetAllServices());
        }
    }

}