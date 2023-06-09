namespace NewConsoleDi;

public record Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public int Age { get; set; }
    public string Gender { get; set; } = null!;
    public string? Address { get; set; }
}