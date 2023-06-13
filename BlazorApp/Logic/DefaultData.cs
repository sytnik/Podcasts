using BlazorApp.DAO;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Person = BlazorApp.DAO.Person;

namespace BlazorApp.Logic;

public static class DefaultData
{
    public static void Populate(this SampleContext context)
    {
        var personFaker = new Faker<Person>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Age, f => f.Random.Int(18, 100))
            .RuleFor(p => p.Gender, f => f.Person.Gender.ToString())
            .RuleFor(p => p.Address, f => f.Address.FullAddress());
        context.SetupEntities(personFaker, out var persons, 200);
        // var persons = personFaker.Generate(200);
        // var maxId = context.Persons.Max(p => p.Id);
        // persons.ForEach(p => p.Id = ++maxId);
        // context.Persons.AddRange(persons);
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Random.Decimal(50, 10000));
        context.SetupEntities(productFaker, out var products, 300);
        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.PersonId, f => f.PickRandom(persons.Select(p => p.Id)))
            .RuleFor(o => o.Info, f => f.Lorem.Sentences());
        context.SetupEntities(orderFaker, out var orders);
        var orderDetailsFaker = new Faker<OrderDetails>()
            .RuleFor(od => od.OrderId, f => f
                .PickRandom(orders.Select(order => order.Id)))
            .RuleFor(od => od.ShippingAddress, f => f.Address.FullAddress());
        context.SetupEntities(orderDetailsFaker, out var orderDetails);
        var orderProductsFaker = new Faker<OrderProduct>()
            .RuleFor(o => o.OrderId,
                f => f.PickRandom(orders.Select(o => o.Id)))
            .RuleFor(o => o.ProductId,
                f => f.PickRandom(products.Select(pr => pr.Id)));
        context.SetupEntities(orderProductsFaker, out var orderProducts, 300, false);
        var orderProductsDb = context.OrderProduct.ToList();
        orderProducts = orderProducts.Except(orderProductsDb).ToList();
        orderProducts = orderProducts
            .DistinctBy(op => new {op.OrderId, op.ProductId}).ToList();
        context.AddRange(orderProducts);
        context.SaveChanges();
    }

    private static void SetupEntities<T>(this DbContext context, Faker<T> faker,
        out List<T> entities, int count = 100, bool addToContext = true)
        where T : EntityWithId
    {
        entities = faker.Generate(count);
        var id = context.Set<T>().Any() ? context.Set<T>().Max(e => e.Id) : 0;
        entities.ForEach(e => e.Id = ++id);
        if (addToContext) context.Set<T>().AddRange(entities);
    }
}