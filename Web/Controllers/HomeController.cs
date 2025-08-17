using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Interfaces;
using Web.Models;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeViewModelService _homeViewModelService;

        public HomeController(
            IHomeViewModelService homeViewModelService)
        {
            _homeViewModelService = homeViewModelService;
        }
        public async Task<IActionResult>Index(IndexViewModel catalogModel, int? pageId)
        {
            var model = await _homeViewModelService.IndexCatalogItems(catalogModel, pageId);
            
            return View(model);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
