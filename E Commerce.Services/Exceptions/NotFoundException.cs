

namespace E_Commerce.Services.Exceptions
{
    public abstract class NotFoundException(string message) : Exception(message)
    {

    }
    public sealed class ProductNotFoundException(int id) : NotFoundException($"Product with Id {id} was not found.")
    {

    }
    public sealed class BasketNotFoundException(string Id) : NotFoundException($"Basket with Id {Id} was not found.")
    {
    }
}
