using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;
using API.Data.Models;

namespace API.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        public ProductService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public Guid CreateNewCategory(CategoryDto categoryDto)
        {

            Asset asset = new Asset
            {
                Id = Guid.NewGuid(),
                FileName = categoryDto.FileName,
                FilePath = categoryDto.FilePath
            };

            Category category = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = categoryDto.CategoryName,
                SortOrder = categoryDto.SortOrder,
                Description = categoryDto.CategoryDescription,
                AssetId = asset.Id

            };
            _repoWrapper.Asset.Create(asset);
            _repoWrapper.Category.Create(category);
            _repoWrapper.Save();
            return category.Id;
        }

        public List<GetCategoryListDto> GetCategoryList()
        {
            List<GetCategoryListDto> category = ( from c in _repoWrapper.Category.FindByCondition(x => x.IsActive)
                                                join a in _repoWrapper.Asset.FindByCondition(x => x.IsActive) on c.AssetId equals a.Id
                                                select new GetCategoryListDto
                                                {
                                                    Id = c.Id,
                                                    CategoryName = c.CategoryName,
                                                    SortOrder = c.SortOrder,
                                                    CategoryDescription = c.Description,
                                                    FilePath = a.FilePath
                                                })
                                                .OrderBy(x => x.SortOrder)
                                                .ToList();

            return category;
        }
        

    }
}