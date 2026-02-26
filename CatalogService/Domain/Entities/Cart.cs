namespace ProductService.Domain.Entities;

public class Cart
{
    public List<Product> Products { get; set; }
    public Guid Id { get; set; }
    public Guid OwnerId { get; init; }

    public Cart(List<Product> products, Guid ownerId)
    {
        Id = Guid.NewGuid();
        Products = products;
        OwnerId = ownerId;
    }
}