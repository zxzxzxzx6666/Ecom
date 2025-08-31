using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.Result;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Interfaces;
using Web.Services;
using Web.ViewModels.Basket;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private IBasketViewModelService _basketViewModelService;
        private readonly IRepository<CatalogItem> _itemRepository;

        public BasketController(IBasketViewModelService basketViewModelService, IHttpClientFactory httpClientFactory, IRepository<CatalogItem> itemRepository)
        {
            _basketViewModelService = basketViewModelService;
            _itemRepository = itemRepository;
        }
        [Authorize]
        public async Task<IActionResult> AddItemToBasket(CatalogItemViewModel productDetails)
        {
            // Get the UserID claim value from the current user
            var username = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            // Get or create basket
            var BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(username);

            // redirect to home if product is not exist
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            var item = await _itemRepository.GetByIdAsync(productDetails.Id);
            if (item == null)
            {
                return RedirectToPage("/Index");
            }

            // add item to basket
            var basket = await _basketViewModelService.AddItemToBasket(username,productDetails.Id, item.Price);

            // add item info to basket model
            //BasketModel = await _basketViewModelService.Map(basket);

            return Json(new { success = true , message = "Success"});
        }
    }
}