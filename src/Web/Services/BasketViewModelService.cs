using System.Security.Claims;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

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

        public BasketViewModelService(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantityId)
        {
            var basket = await _basketService.AddItemToBasketAsync(BuyerId, productId, quantityId);

            return new BasketViewModel()
            {
                Id = basket.Id,
                BuyerId = BuyerId,
                Items = basket.Items.Select(x => new BasketItemViewModel()
                {
                    Id = x.Id,
                    PictureUri = x.Product.PictureUri,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.Product.Price
                }).ToList()
            };
        }
    }
}
