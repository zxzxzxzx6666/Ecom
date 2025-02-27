namespace ApplicationCore.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<bool> SignUp(string userName, string email, string passWord);
    }
}
