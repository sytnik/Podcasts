namespace BlazorApp.DAO;

public sealed record OrderDetails : EntityWithId
{
    public string ShippingAddress { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}