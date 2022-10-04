using System.Security.Claims;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderService _orderService;

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;
        private string? UserId => HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string? AnonId => HttpContext?.Request.Cookies[Constants.BASKET_COOKIENAME];
        private string BuyerId => UserId ?? AnonId ?? CreateAnonymousId();

        private string creatednonId = null!;

        private string CreateAnonymousId()
        {
            if (creatednonId == null)
            {
                creatednonId = Guid.NewGuid().ToString();

                HttpContext?.Response.Cookies.Append(Constants.BASKET_COOKIENAME, creatednonId, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(14),
                    IsEssential = true
                });
            }

            return creatednonId;
        }

        public BasketViewModelService(IBasketService basketService, IHttpContextAccessor httpContextAccessor, IOrderService orderService)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
        }
        public async Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantityId)
        {
            var basket = await _basketService.AddItemToBasketAsync(BuyerId, productId, quantityId);
            return basket.ToBasketViewModel();
        }

        public async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            var basket = await _basketService.GetOrCreateBasketAsync(BuyerId);
            return basket.ToBasketViewModel();
        }

        public async Task EmptyBasketAsync()
        {
            await _basketService.EmptyBasketAsync(BuyerId);
        }

        public async Task RemoveBasketItemAsync(int productId)
        {
            await _basketService.DeleteBasketItemAsync(BuyerId, productId);
        }

        public async Task<BasketViewModel> UpdateBasketAsync(Dictionary<int, int> quantities)
        {
            var basket = await _basketService.SetQuantities(BuyerId, quantities);
            return basket.ToBasketViewModel();
        }

        public async Task TransferBasketasync()
        {
            if (AnonId != null && UserId != null)
            {
                await _basketService.TransferBasketasync(AnonId, UserId);
                HttpContext?.Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
            }
        }

        public async Task CompleteCheckoutAsync(string street, string city, string state, string country, string zipCode)
        {
            var shippingAddress = new Address(street, city, state, country, zipCode);
            await _orderService.CreateOrderAsync(BuyerId, shippingAddress);
            await _basketService.EmptyBasketAsync(BuyerId);
        }
    }
}
