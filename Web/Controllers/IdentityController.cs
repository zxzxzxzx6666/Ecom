using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Interfaces;
using Web.ViewModels.Identity;

namespace Web.Controllers
{
    public class IdentityController : Controller
    {
        private IIdendityViewModelService _IdendityViewModelService;
        
        public IdentityController(IIdendityViewModelService idendityViewModelService, IHttpClientFactory httpClientFactory)
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
            // Check if the password and confirm password match
            if (model.PassWord != model.ConfirmPassWord)
            {
                ModelState.AddModelError("ConfirmPassWord", "Passwords do not match.");
            }

            if (ModelState.IsValid)
            {
                // Handle the registration logic here, such as storing in the database
                _IdendityViewModelService.SignUp(model).GetAwaiter().GetResult();

                // Redirect to login page after successful signup
                return RedirectToAction("Login", "Identity");
            }

            // If validation fails, redisplay the form with error messages
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
                // return token
                var result = _IdendityViewModelService.Login(model).GetAwaiter().GetResult();

                if (result.IsSucessfull)
                {
                    // 建立 ClaimsIdentity，這是包含使用者身份與角色資訊的物件
                    var claimsIdentity = new ClaimsIdentity(result.ClaimItem, CookieAuthenticationDefaults.AuthenticationScheme);

                    // 將登入資訊寫入 Cookie 中，完成登入程序
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, // 認證機制（Cookie）
                        new ClaimsPrincipal(claimsIdentity) // 使用者身份（包含 Claims）
                    ).GetAwaiter().GetResult();                       

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