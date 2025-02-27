using ApplicationCore.Interfaces;
using ApplicationCore.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.IO;
using System.Threading.Channels;

namespace ApplicationCore.Entities.IdentityAggregate;
/// <summary>
/// [aggregate root]
/// 1. The only entry point for aggregation
///     Internal entities cannot be modified directly from the outside. 
///     All changes must be performed through the Aggregate Root 
/// 2. Encapsulate business logic: 
///     Responsible for key business rules.
/// 3. life cycle
///     The life cycle (create update delete) of internal entities is controlled by Aggregate Root.
/// 4. Access through Repository: 
///     Repository only processes Aggregate Root and does not directly operate internal entities 
///     to ensure data integrity.
/// </summary>
public class IdentityInfo : BaseEntity, IAggregateRoot
{
    #region db field
    public string UserId { get; private set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    #endregion
    /// <summary>
    /// Initializes a new instance of the with the specified user ID.
    /// </summary>
    /// <param name="userId"></param>
    public IdentityInfo(string userId)
    {
        UserId = userId;
    }
    #region domain : business logic
    /// <summary>
    /// create new account
    /// This method encapsulates the business logic required to create a user account
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="passWord"></param>
    public void CreateAccount(string userName, string email, string passWord)
    {
        UserName = userName;
        Email = email;
        HashedPassword = PassWordHelper.HashPassword(passWord);
    }
    #endregion
}
