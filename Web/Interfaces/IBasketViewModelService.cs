using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Basket;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<IActionResult> AddItemToBasket();
    }
}
