namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetBasketViewModelAsync();
        Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantityId);
        Task EmptyBasketAsync();
        Task RemoveBasketItemAsync(int productId);
        Task<BasketViewModel> UpdateBasketAsync(Dictionary<int, int> quantities);
        Task TransferBasketasync();
        Task CompleteCheckoutAsync(string street, string city, string state, string country, string zipCode);
    }
}
