using API.Contract.IService;
using API.Data.Dto;
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
        [HttpPost]
        [Route("api/product/category")]
        public IActionResult CreateNewCategory([FromBody] CategoryDto categoryDto)
        {
            //   Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok(_productService.CreateNewCategory(categoryDto));

        }
        [HttpGet]
        [Route("api/product/category")]
        public IActionResult GetCategoryList()
        {
            //   Guid userId =Guid.Parse(User.FindFirst("userId")?.Value);
            return Ok(_productService.GetCategoryList());

        }

    }
}