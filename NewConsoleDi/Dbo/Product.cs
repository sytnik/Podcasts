namespace NewConsoleDi.Dbo;

public sealed class Product : EntityWithId
{
    public string Name { get; set; }
    public int ShopId { get; set; }
    public Shop Shop { get; set; }
    public decimal Price { get; set; }
    public List<Order> Orders { get; set; }
}