using ApplicationCore.Entities.IdentityAggregate;
using ApplicationCore.Models.Identity;

namespace ApplicationCore.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<bool> SignUp(string userName, string email, string passWord);
        Task<LoginResultModel> Login(string userName, string passWord);
    }
}
