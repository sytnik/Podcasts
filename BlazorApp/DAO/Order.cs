namespace BlazorApp.DAO;

public sealed record Order : EntityWithId
{
    public int PersonId { get; set; }

    public string? Info { get; set; }

    public Person Person { get; set; }

    public OrderDetails OrderDetails { get; set; }

    public List<Product> Products { get; set; }
}