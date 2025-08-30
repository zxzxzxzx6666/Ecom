using Web.Interfaces;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Specifications;
using ApplicationCore.Services;

namespace Web.Services;

public class BasketViewModelService : IBasketViewModelService
{
    private readonly IRepository<Basket> _basketRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IBasketQueryService _basketQueryService;
    private readonly IRepository<CatalogItem> _itemRepository;

    public BasketViewModelService(IRepository<Basket> basketRepository,
        IRepository<CatalogItem> itemRepository,
        IUriComposer uriComposer,
        IBasketQueryService basketQueryService)
    {
        _basketRepository = basketRepository;
        _uriComposer = uriComposer;
        _basketQueryService = basketQueryService;
        _itemRepository = itemRepository;
    }
    public async Task<Basket> AddItemToBasket(string username, int catalogItemId, decimal price, int quantity = 1)
    {
        // check if this user's basket exists
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

        // if not, create a new one
        if (basket == null)
        {
            basket = new Basket(username);
            await _basketRepository.AddAsync(basket);
        }

        // add item to basket
        basket.AddItem(catalogItemId, price, quantity);

        await _basketRepository.UpdateAsync(basket);
        return basket;
    }
    public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
    {
        var basketSpec = new BasketWithItemsSpecification(userName);
        var basket = (await _basketRepository.FirstOrDefaultAsync(basketSpec));

        if (basket == null)
        {
            return await CreateBasketForUser(userName);
        }
        var viewModel = await Map(basket);
        return viewModel;
    }
    private async Task<BasketViewModel> CreateBasketForUser(string userId)
    {
        var basket = new Basket(userId);
        await _basketRepository.AddAsync(basket);

        return new BasketViewModel()
        {
            BuyerId = basket.BuyerId,
            Id = basket.Id,
        };
    }
    /// <summary>
    /// Map Basket and Basket item 
    /// </summary>
    /// <param name="basket"></param>
    /// <returns></returns>
    public async Task<BasketViewModel> Map(Basket basket)
    {
        return new BasketViewModel()
        {
            BuyerId = basket.BuyerId,
            Id = basket.Id,
            Items = await GetBasketItems(basket.Items)
        };
    }
    private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
    {
        var catalogItemsSpecification = new CatalogItemsSpecification(basketItems.Select(b => b.CatalogItemId).ToArray());
        var catalogItems = await _itemRepository.ListAsync(catalogItemsSpecification);

        var items = basketItems.Select(basketItem =>
        {
            var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);

            var basketItemViewModel = new BasketItemViewModel
            {
                Id = basketItem.Id,
                UnitPrice = basketItem.UnitPrice,
                Quantity = basketItem.Quantity,
                CatalogItemId = basketItem.CatalogItemId,
                PictureUrl = _uriComposer.ComposePicUri(catalogItem.PictureUri),
                ProductName = catalogItem.Name
            };
            return basketItemViewModel;
        }).ToList();

        return items;
    }
}
