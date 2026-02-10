namespace CatalogService.Models;

public sealed class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public List<Uri> Images { get; set; }
    public Category Category { get; set; }
    public Product(string name, string description, decimal price,  List<Uri> images, Category category)
    {
        Name = name;
        Description = description;
        Price = price;
        Images = images;
        Id = Guid.NewGuid();
        Category = category;
    }
}