using ApplicationCore.Entities.IdentityAggregate;
using ApplicationCore.Models.Identity;
using Web.ViewModels.Identity;

namespace Web.Interfaces
{
    public interface IIdendityViewModelService
    {
        Task<bool> SignUp(SignUpViewModel model);
        Task<LoginResultModel> Login(LoginViewModel model);
    }
}
