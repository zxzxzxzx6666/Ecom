using ApplicationCore.Entities.IdentityAggregate;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models.Identity;
using ApplicationCore.Specifications.Identity;


namespace ApplicationCore.Services
{
    /// <summary>
    /// [Application Service]
    ///     1. Application use cases
    ///     2. Combine domain objects (Entities, Aggregates)
    ///     3. Interact with the infrastructure layer
    ///     4. Responsible for transaction management 
    ///     5. Provide application logic for external (Controller, API)
    /// </summary>
    public class IdentityService : IIdentityService
    {
        #region Dependency Injection
        private readonly IRepository<IdentityInfos> _IdentityInfo;
        public IdentityService(IRepository<IdentityInfos> identityInfo)
        {
            _IdentityInfo = identityInfo;
        }
        #endregion
        public async Task<bool> SignUp(string userName, string email, string passWord)
        {
            try
            {
                var identityInfo = new IdentityInfos(Guid.NewGuid().ToString());
                identityInfo.CreateAccount(userName, email, passWord, Enums.UserRole.User.ToString());
                await _IdentityInfo.AddAsync(identityInfo);
                return true;
            }
            catch (Exception ex)
            {
                // todo : log 記錄錯誤訊息
                return false;
            }
        }
        public async Task<LoginResultModel> Login(string email, string passWord)
        {
            var result = new LoginResultModel();
            try
            {
                // find user by email
                var getUser = new FindUserSpecification(email);
                var user = await _IdentityInfo.FirstOrDefaultAsync(getUser);

                // verify user
                if (user == null)
                {
                    result.IsSucessfull = false;
                    result.Message = "User not found!";
                    return result;
                }
                // verify password
                if (!PassWordHelper.VerifyPassword(passWord, user.HashedPassword))
                {
                    result.IsSucessfull = false;
                    result.Message = "Wrong Password!";
                    return result;
                }

                // fill out result
                //result.Token = JwtHelper.GenerateJwtToken(user.UserId.ToString(), user.UserName, user.UserRoles.Select(x => x.RoleName).FirstOrDefault()); todo : 改 jwt 用
                result.ClaimItem = JwtHelper.GenerateClaims(user.UserId.ToString(), user.UserName, user.UserRoles.Select(x => x.RoleName).FirstOrDefault());
                result.IsSucessfull = true;
                result.Roles = user.UserRoles.Select(x => x.RoleName).ToList();

                return result;
            }
            catch (Exception)
            {
                result.IsSucessfull = false;
                return result;
            }
        }
    }
}
