using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "請輸入使用者名稱")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "請輸入電子郵件")]
        [EmailAddress(ErrorMessage = "電子郵件格式不正確")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "密碼長度至少 6 個字元")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "請再次輸入密碼")]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "兩次密碼輸入不一致")]
        public string ConfirmPassWord { get; set; }
    }
}
