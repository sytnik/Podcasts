namespace NewConsoleDi.Dbo;

public class ShopContext(DbContextOptions<ShopContext> options) : DbContext(options)
{
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Shop> Shop { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>().Property(product => product.Price).HasColumnType("decimal(18,2)");
        builder.Entity<Order>()
            .HasMany(order => order.Products)
            .WithMany(product => product.Orders)
            .UsingEntity<OrderProduct>();
    }
}