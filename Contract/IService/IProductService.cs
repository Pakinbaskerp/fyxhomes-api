using API.Data.Dto;

namespace API.Contract.IService
{
    public interface IProductService
    {
         Guid CreateNewCategory(CategoryDto categoryDto);
         List<GetCategoryListDto> GetCategoryList();
         Guid CreateService(ServiceDto serviceDto, Guid categoryId);
         List<ServiceDto> GetListOfServicesByCategory(Guid categoryId);

         List<GetAllServiceListDto> GetAllServices();
        
         
    }
}