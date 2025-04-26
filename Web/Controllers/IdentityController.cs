using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.Services;
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // todo return (true token message)
                var result = _IdendityViewModelService.Login(model).GetAwaiter().GetResult();

                if (result.IsSucessfull)
                {
                    // 存入 Cookie
                    HttpContext.Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddHours(2),
                        SameSite = SameSiteMode.Strict
                    });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = result.Message;
                }
            }

            return View(model);
        }

    }
}