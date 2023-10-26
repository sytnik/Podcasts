namespace NewConsoleDi.Dbo;

public sealed class Customer : EntityWithId
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public List<Order> Orders { get; set; }
}