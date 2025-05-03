using System.Threading.Tasks;
using ApplicationCore.Entities.OrderAggregate;

namespace ApplicationCore.Interfaces 
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int basketId, Address shippingAddress);
    }

}

