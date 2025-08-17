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

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private IBasketViewModelService _BasketViewModelService;
        
        public BasketController(IBasketViewModelService basketViewModelService, IHttpClientFactory httpClientFactory)
        {
            _BasketViewModelService = basketViewModelService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItemToBasket()
        {
            // if there no productDetails?.Id then RedirectToPage("/Index")

            // get productDetails.Id for productDetails

            // if there no item then RedirectToPage("/Index")

            // if there no product id then RedirectToPage("/Index")

            //get item by productDetails.Id

            // if there no item then RedirectToPage("/Index")

            // get user information from cookie
            // 是否已登入
            bool isAuth = User.Identity?.IsAuthenticated == true;
            // 常見 Claims
            string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            string? userName = User.Identity?.Name  // 等同於 Name claim
                             ?? User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            string? email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            // 角色
            bool isAdmin = User.IsInRole("Admin");
            // 自訂 Claim
            string? tenantId = User.FindFirst("tenant_id")?.Value;

            // Add basket item (move it to service)
            //var basket = await _basketService.AddItemToBasket(username,
            //productDetails.Id, item.Price);

            // return true or false
            return Json(new { success = true });
        }
    }
}