using ApplicationCore.Entities.IdentityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Services;


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
        private readonly IRepository<IdentityInfo> _IdentityInfo;
        public IdentityService(IRepository<IdentityInfo> identityInfo)
        {
            _IdentityInfo = identityInfo;
        }
        #endregion
        public async Task<bool> SignUp(string userName, string email, string passWord)
        {
            try
            {
                var identityInfo = new IdentityInfo(Guid.NewGuid().ToString());
                identityInfo.CreateAccount(userName, email, passWord);
                await _IdentityInfo.AddAsync(identityInfo);
                return true;
            }
            catch (Exception ex)
            {
                // todo : log 記錄錯誤訊息
                return false;
            }
        }
    }
}
