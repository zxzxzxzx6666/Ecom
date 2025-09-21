using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.ViewModels.Basket;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private IBasketViewModelService _basketViewModelService;
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IBasketService _basketService;

        public BasketController(IBasketViewModelService basketViewModelService, IHttpClientFactory httpClientFactory, IRepository<CatalogItem> itemRepository, IBasketService basketService)
        {
            _basketViewModelService = basketViewModelService;
            _itemRepository = itemRepository;
            _basketService = basketService;
        }
        #region page
        /// <summary>
        /// bastet page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(username);
            return View(BasketModel);
        }
        /// <summary>
        /// bastet page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(username);
            return View(BasketModel);
        }
        #endregion
        #region index api
        /// <summary>
        /// user add item to their basket
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
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
            var basket = await _basketViewModelService.AddItemToBasket(username, productDetails.Id, item.Price);

            // add item info to basket model
            //BasketModel = await _basketViewModelService.Map(basket);

            return Json(new { success = true, message = "Success" });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody] IEnumerable<BasketItemModel> items)
        {
            // check model state
            if (!ModelState.IsValid)
            {
                Json(new { success = true, message = "fail" });
            }

            // get basket by user
            var username = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var basketView = await _basketViewModelService.GetOrCreateBasketForUser(username);

            // update quantities
            var updateModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
            var basket = await _basketService.SetQuantities(basketView.Id, updateModel);
            return Json(new { success = true, message = "Success" });
        }
        #endregion
    }
}