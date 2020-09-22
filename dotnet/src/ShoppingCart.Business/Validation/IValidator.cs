namespace ShoppingCart.Business.Validation
{
    public interface IValidator<in TRequest>
    {
        void Validate(TRequest request);
    }
}