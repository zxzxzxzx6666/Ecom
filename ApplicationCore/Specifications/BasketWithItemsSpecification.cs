using Ardalis.Specification;
using ApplicationCore.Entities.BasketAggregate;

namespace ApplicationCore.Specifications 
{
    public sealed class BasketWithItemsSpecification : Specification<Basket>
    {
        public BasketWithItemsSpecification(int basketId)
        {
            Query
                .Where(b => b.Id == basketId)
                .Include(b => b.Items);
        }
        /// <summary>
        /// 依 BuyerId 查詢 Basket，並引入 Items 對應的資料
        /// </summary>
        /// <param name="buyerId"></param>
        public BasketWithItemsSpecification(string buyerId)
        {
            Query
                .Where(b => b.BuyerId == buyerId)
                .Include(b => b.Items);
        }
    }

}

