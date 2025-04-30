using ApplicationCore.Interfaces;
using ApplicationCore.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.IO;
using System.Threading.Channels;

namespace ApplicationCore.Entities.IdentityAggregate;
/// <summary>
/// [Entity]

/// </summary>
public class Roles : BaseEntity
{
    #region db field
    public string RoleName { get; private set; }
    public string UserId { get; set; }
    #endregion

    #region domain : business logic
    /// <summary>
    /// Initializes a new instance of the with the specified user ID.
    /// for now UserId RoleName is one to one
    /// </summary>
    /// <param name="userId"></param>
    public Roles(string roleName, string userId)
    {
        RoleName = roleName;
        UserId = userId;
    }
    #endregion
}
