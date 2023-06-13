using Microsoft.EntityFrameworkCore;

namespace BlazorApp.DAO;

public sealed class SampleContext : DbContext
{
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>()
            .HasOne(order => order.OrderDetails)
            .WithOne(details => details.Order)
            .HasForeignKey<OrderDetails>(details => details.OrderId);
        builder.Entity<Order>()
            .HasOne(order => order.Person)
            .WithMany(person => person.Orders)
            .HasForeignKey(order => order.PersonId);
        builder.Entity<Order>()
            .HasMany(order => order.Products)
            .WithMany(product => product.Orders)
            .UsingEntity<OrderProduct>(
                typeBuilder => typeBuilder
                    .HasOne(orderProduct => orderProduct.Product).WithMany()
                    .HasForeignKey(orderProduct => orderProduct.ProductId),
                typeBuilder => typeBuilder
                    .HasOne(orderProduct => orderProduct.Order).WithMany()
                    .HasForeignKey(orderProduct => orderProduct.OrderId),
                typeBuilder => typeBuilder
                    .HasKey(orderProduct =>
                        new {orderProduct.ProductId, orderProduct.OrderId}));
    }

    public SampleContext(DbContextOptions<SampleContext> options) : base(options)
    {
    }
}