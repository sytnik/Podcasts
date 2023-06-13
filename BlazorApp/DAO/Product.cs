namespace BlazorApp.DAO;

public sealed record Product : EntityWithId
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<Order> Orders { get; set; }
}