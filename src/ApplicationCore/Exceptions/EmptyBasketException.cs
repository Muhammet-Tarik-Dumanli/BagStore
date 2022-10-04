namespace ApplicationCore.Exceptions
{
    public class EmptyBasketException : Exception
    {
        public EmptyBasketException() : base("Basket cannot be empty.")
        {

        }
    }
}
