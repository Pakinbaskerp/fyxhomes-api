using API.Data.Dto;

namespace API.Contract.IService
{
    public interface IProductService
    {
         Guid CreateNewCategory(CategoryDto categoryDto);
         List<GetCategoryListDto> GetCategoryList();
         
    }
}