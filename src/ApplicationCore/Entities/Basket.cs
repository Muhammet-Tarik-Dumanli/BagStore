namespace ApplicationCore.Entities
{
    public class Basket : BaseEntity
    {
        public string BuyerId { get; set; } = null!;
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
