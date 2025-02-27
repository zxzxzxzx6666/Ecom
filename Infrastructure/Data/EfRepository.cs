using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// [Repository]
    ///     1. Separating data sources (db) from business logic    
    ///     2. Data access abstraction Repository provides an interface for abstracting data access
    ///     3. Must be IAggregateRoot
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(WebContext dbContext) : base(dbContext)
        {
        }
    }

}
