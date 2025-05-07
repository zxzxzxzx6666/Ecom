using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Queries 
{
    public class BasketQueryService : IBasketQueryService
    {
        private readonly WebContext _dbContext;

        public BasketQueryService(WebContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method performs the sum on the database rather than in memory
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<int> CountTotalBasketItems(string username)
        {
            var totalItems = await _dbContext.Baskets
                .Where(basket => basket.BuyerId == username)
                .SelectMany(item => item.Items)
                .SumAsync(sum => sum.Quantity);

            return totalItems;
        }
    }
}


