namespace MockShop.Shared.DAO;

public sealed record Person : EntityWithId
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public List<Order> Orders { get; set; }
}