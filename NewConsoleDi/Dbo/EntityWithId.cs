namespace NewConsoleDi.Dbo;

public class EntityWithId
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
}