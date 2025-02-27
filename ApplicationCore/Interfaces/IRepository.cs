using Ardalis.Specification;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// [Repository interface]
    ///     inheriting IRepositoryBase<T> of Ardalis.Specification to implement
    ///     CRUD operations of Aggregate Root.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
