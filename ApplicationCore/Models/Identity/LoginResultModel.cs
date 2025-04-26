namespace ApplicationCore.Models.Identity
{
    public class LoginResultModel
    {
        public bool IsSucessfull { get; set; }
        public string Message { get; set; } 
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
