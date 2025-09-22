using ApplicationCore.Entities.OrderAggregate;

namespace Web.Interfaces
{
    public interface IOrderViewModelService
    {
        Task CreateOrderAsync(int basketId, Address shippingAddress);
    }
}
