namespace NewConsoleDi.Dbo;

public sealed class OrderProduct
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    public Product Product { get; set; }
    public Order Order { get; set; }
}