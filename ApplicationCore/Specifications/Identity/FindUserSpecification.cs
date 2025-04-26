using Ardalis.Specification;
using ApplicationCore.Entities.IdentityAggregate;

namespace ApplicationCore.Specifications.Identity
{
    /// <summary>
    /// Specification is one of the design patterns in DDD, mainly used to:
    ///     1. Define query conditions without polluting business logic.
    ///     2. Allow different query conditions to be reused to reduce duplicate code.
    /// </summary>
    public class FindUserSpecification : Specification<IdentityInfos>
    {
        /// <summary>
        /// find user by email
        /// </summary>
        /// <param name="Email"></param>
        public FindUserSpecification(string Email)
        {
            Query
                .Where(b => b.Email == Email)
                .Include(b => b.UserRoles);
        }
    }
}
