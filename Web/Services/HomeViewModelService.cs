using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web;
using Microsoft.Extensions.Logging;
using Web.Controllers;
using Web.Interfaces;
using Web.ViewModels.Home;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        #region dependency injection
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IRepository<CatalogBrand> _brandRepository;
        private readonly IRepository<CatalogType> _typeRepository;
        private readonly IUriComposer _uriComposer;
        private readonly ILogger<HomeController> _logger;
        public HomeViewModelService(
            IRepository<CatalogItem> itemRepository,
            IRepository<CatalogBrand> brandRepository,
            IRepository<CatalogType> typeRepository,
            IUriComposer uriComposer,
            ILogger<HomeController> logger)
        {
            _itemRepository = itemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _uriComposer = uriComposer;
            _logger = logger;
        }
        #endregion
        #region main services
        public async Task<IndexViewModel> IndexCatalogItems(IndexViewModel catalogModel, int? pageId)
        {
            int pageIndex = pageId ?? 0;
            int itemsPage = Constants.ITEMS_PER_PAGE;
            int? brandId = catalogModel.BrandFilterApplied;
            int? typeId = catalogModel.TypesFilterApplied;
            _logger.LogInformation("GetCatalogItems called.");

            var filterSpecification = new CatalogFilterSpecification(brandId, typeId);
            var filterPaginatedSpecification =
                new CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, brandId, typeId);

            // the implementation below using ForEach and Count. We need a List.
            var itemsOnPage = await _itemRepository.ListAsync(filterPaginatedSpecification);
            var totalItems = await _itemRepository.CountAsync(filterSpecification);

            var model = new IndexViewModel()
            {
                CatalogItems = itemsOnPage.Select(i => new CatalogItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                    Price = i.Price
                }).ToList(),
                Brands = (await GetBrands()).ToList(),
                Types = (await GetTypes()).ToList(),
                BrandFilterApplied = brandId ?? 0,
                TypesFilterApplied = typeId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            model.PaginationInfo.Next = (model.PaginationInfo.ActualPage == model.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            model.PaginationInfo.Previous = (model.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            
            return model;
        }
        #endregion
        #region helper methods
        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            _logger.LogInformation("GetBrands called.");
            var brands = await _brandRepository.ListAsync();

            var items = brands
                .Select(brand => new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            _logger.LogInformation("GetTypes called.");
            var types = await _typeRepository.ListAsync();

            var items = types
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Type })
                .OrderBy(t => t.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
        #endregion
    }
}
