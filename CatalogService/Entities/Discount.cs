namespace ProductService.Entities;

public class Discount
{
    public List<Product> Products { get; set; }
    public byte Percentage { get; set; }
    
    public Guid Id { get; set; }

    public Discount(List<Product> products, byte percentage)
    {
        Products = products;
        Percentage = percentage;
        Id = Guid.NewGuid();
    }
}