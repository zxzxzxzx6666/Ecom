using ApplicationCore.Interfaces;
using ApplicationCore.Extensions;

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
public class IdentityInfos : BaseEntity, IAggregateRoot
{
    #region db field
    public string UserId { get; private set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    #endregion
    #region entity
    // DDD Patterns comment
    // Using a private collection field, better for DDD Aggregate's encapsulation
    // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
    // but only through the method Order.AddOrderItem() which includes behavior.
    private readonly List<Roles> _userRoles = new List<Roles>();

    // Using List<>.AsReadOnly() 
    // This will create a read only wrapper around the private list so is protected against "external updates".
    // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
    public IReadOnlyCollection<Roles> UserRoles => _userRoles.AsReadOnly();
    #endregion
    /// <summary>
    /// Initializes a new instance of the with the specified user ID.
    /// </summary>
    /// <param name="userId"></param>
    public IdentityInfos(string userId)
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
    /// <param name="role"></param>
    public void CreateAccount(string userName, string email, string passWord, string role)
    {
        UserName = userName;
        Email = email;
        HashedPassword = PassWordHelper.HashPassword(passWord);
        _userRoles.Add(new Roles(role, UserId));
    }
    #endregion
}
