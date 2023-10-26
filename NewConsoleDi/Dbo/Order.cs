namespace NewConsoleDi.Dbo;

public sealed class Order : EntityWithId
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime Date { get;set; }
    public string Address { get; set; }
    public List<Product> Products { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}