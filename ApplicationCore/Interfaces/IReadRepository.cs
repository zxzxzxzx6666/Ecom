using Ardalis.Specification;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// [Read-only Repository interface]
    ///     inheriting IReadRepositoryBase<T> of Ardalis.Specification to provide 
    ///     read-only data access to ensure that query operations do not affect the data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
