namespace BlazorApp.DAO;

public sealed record OrderProduct : EntityWithId
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
}