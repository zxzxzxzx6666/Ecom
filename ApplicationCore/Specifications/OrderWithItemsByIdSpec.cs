using Ardalis.Specification;
using ApplicationCore.Entities.OrderAggregate;

namespace ApplicationCore.Specifications 
{
    public class OrderWithItemsByIdSpec : Specification<Order>
    {
        public OrderWithItemsByIdSpec(int orderId)
        {
            Query
                .Where(order => order.Id == orderId)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.ItemOrdered);
        }
    }

}
