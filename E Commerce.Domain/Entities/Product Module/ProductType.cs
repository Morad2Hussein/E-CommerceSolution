using E_Commerce.Domain.Entities.Shared;


namespace E_Commerce.Domain.Entities.Product_Module
{
    public class ProductType :BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
