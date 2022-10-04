using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(string buyerId, Address shippingAddress);
    }
}
