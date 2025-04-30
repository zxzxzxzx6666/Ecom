using Web.Interfaces;
using ApplicationCore.Interfaces.Services;
using Web.ViewModels.Identity;
using ApplicationCore.Models.Identity;

namespace Web.Services;

public class IdendityViewModelService : IIdendityViewModelService
{
    private readonly IIdentityService _IdentityService;
    public IdendityViewModelService(IIdentityService identityService)
    {
        _IdentityService = identityService;
    }

    public async Task<bool> SignUp(SignUpViewModel model) 
    {
        return await _IdentityService.SignUp(model.UserName, model.Email, model.PassWord);
    }
    
    public async Task<LoginResultModel> Login(LoginViewModel model)
    {
        return await _IdentityService.Login(model.Email, model.PassWord);
    }
    //todo : logout function
}
