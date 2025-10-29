
namespace E_Commerce.Domain.Entities.Shared
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}
