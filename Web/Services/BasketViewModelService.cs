using Web.Interfaces;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Web.Services;

public class BasketViewModelService : IBasketViewModelService
{
    private readonly IBasketService _basketService;
    public BasketViewModelService(IBasketService basketService)
    {
        _basketService = basketService;
    }
}
