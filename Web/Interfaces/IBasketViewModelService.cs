using ApplicationCore.Entities.BasketAggregate;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Basket;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<Basket> AddItemToBasket(string username, int catalogItemId, decimal price, int quantity = 1);
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
        Task<BasketViewModel> Map(Basket basket);
    }
}
