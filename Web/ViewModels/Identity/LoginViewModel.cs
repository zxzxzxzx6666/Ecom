using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Identity
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
    }
}
