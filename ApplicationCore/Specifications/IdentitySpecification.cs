using Ardalis.Specification;
using ApplicationCore.Entities.IdentityAggregate;

namespace ApplicationCore.Specifications
{
    /// <summary>
    /// Specification is one of the design patterns in DDD, mainly used to:
    ///     1. Define query conditions without polluting business logic.
    ///     2. Allow different query conditions to be reused to reduce duplicate code.
    /// </summary>
    public class IdentityInfoSpecification : Specification<IdentityInfo>
    {
        public IdentityInfoSpecification(string UserId) : base()
        {
            Query
                .Where(b => b.UserId == UserId);
        }
    }
}
