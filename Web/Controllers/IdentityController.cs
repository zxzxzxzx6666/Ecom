using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.ViewModels.Identity;

namespace Web.Controllers
{
    public class IdentityController : Controller
    {
        private IIdendityViewModelService _IdendityViewModelService;
        public IdentityController(IIdendityViewModelService idendityViewModelService) 
        {
            _IdendityViewModelService = idendityViewModelService;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Handle the registration logic here, such as storing in the database
                _IdendityViewModelService.SignUp(model).GetAwaiter().GetResult();

                return View("Signup", model);
                //return RedirectToAction("Login", "Account");
            }

            // If validation fails, redisplay the form
            return View("Signup", model);
        }
    }
}
