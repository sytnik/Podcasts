namespace BlazorApp.DAO;

public record EntityWithId
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
}