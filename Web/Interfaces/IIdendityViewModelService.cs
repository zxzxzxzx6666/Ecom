using Web.ViewModels.Identity;

namespace Web.Interfaces
{
    public interface IIdendityViewModelService
    {
        Task<bool> SignUp(SignUpViewModel model);
    }
}
