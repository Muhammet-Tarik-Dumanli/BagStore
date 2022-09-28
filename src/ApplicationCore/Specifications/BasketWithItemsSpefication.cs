using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class BasketWithItemsSpefication : Specification<Basket>
    {
        public BasketWithItemsSpefication(string buyerId)
        {
            Query.Where(x => x.BuyerId == buyerId)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product);
        }
    }
}
