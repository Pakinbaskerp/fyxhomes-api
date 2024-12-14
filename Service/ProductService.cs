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
            List<GetCategoryListDto> category = (from c in _repoWrapper.Category.FindByCondition(x => x.IsActive)
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

        public Guid CreateService(ServiceDto serviceDto, Guid categoryId)
        {
            Asset asset = new Asset
            {
                Id = Guid.NewGuid(),
                FileName = serviceDto.FileName,
                FilePath = serviceDto.FilePath,
            };
            PriceCatalogue priceCatalogue = new PriceCatalogue
            {
                Id = Guid.NewGuid(),
                Price = serviceDto.Price,
                Currency = serviceDto.Currency,
            };
            Services services = new Services
            {
                Id = Guid.NewGuid(),
                AssetId = asset.Id,
                PriceId = priceCatalogue.Id,
                ServiceName = serviceDto.ServiceName,
                Description = serviceDto.ServiceDescription,
                SortOrder = serviceDto.SortOrder,
                CategoryId = categoryId
            };
            _repoWrapper.Asset.Create(asset);
            _repoWrapper.PriceCatalogue.Create(priceCatalogue);
            _repoWrapper.Services.Create(services);
            _repoWrapper.Save();

            return services.Id;
        }

        public List<ServiceDto> GetListOfServicesByCategory(Guid categoryId)
        {
            List<ServiceDto> services = (from s in _repoWrapper.Services.FindByCondition(x => x.IsActive && x.CategoryId.Equals(categoryId))
                                         join p in _repoWrapper.PriceCatalogue.FindByCondition(x => x.IsActive) on s.PriceId equals p.Id
                                         join a in _repoWrapper.Asset.FindByCondition(x => x.IsActive) on s.AssetId equals a.Id
                                         select new ServiceDto
                                         {
                                             Id = s.Id,
                                             ServiceName = s.ServiceName,
                                             ServiceDescription = s.Description,
                                             SortOrder = s.SortOrder,
                                             FileName = a.FileName,
                                             FilePath = a.FilePath,
                                             Price = p.Price,
                                             Currency = p.Currency

                                         }).ToList();

            return services;
        }

        public List<GetAllServiceListDto> GetAllServices()
        {
            List<GetCategoryListDto> categories = (from c in _repoWrapper.Category.FindByCondition(x => x.IsActive)
                                                   join a in _repoWrapper.Asset.FindByCondition(x => x.IsActive) on c.AssetId equals a.Id
                                                   select new GetCategoryListDto
                                                   {
                                                       Id = c.Id,
                                                       CategoryName = c.CategoryName,
                                                       SortOrder = c.SortOrder,
                                                       CategoryDescription = c.Description,
                                                       FilePath = a.FilePath
                                                   }).OrderBy(x => x.SortOrder).ToList();

            List<GetAllServiceListDto> result = categories.Select(category => new GetAllServiceListDto
            {
                Category = category,
                Services = (from s in _repoWrapper.Services.FindByCondition(x => x.IsActive && x.CategoryId.Equals(category.Id))
                            join p in _repoWrapper.PriceCatalogue.FindByCondition(x => x.IsActive) on s.PriceId equals p.Id
                            join a in _repoWrapper.Asset.FindByCondition(x => x.IsActive) on s.AssetId equals a.Id
                            select new ServiceDto
                            {
                                Id = s.Id,
                                ServiceName = s.ServiceName,
                                ServiceDescription = s.Description,
                                SortOrder = s.SortOrder,
                                FileName = a.FileName,
                                FilePath = a.FilePath,
                                Price = p.Price,
                                Currency = p.Currency
                            }).ToList()
            }).ToList();

            return result;
        }

    }
}