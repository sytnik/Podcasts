using Bogus;
using Microsoft.EntityFrameworkCore;
using MockShop.Server.DAO;
using MockShop.Shared.DAO;
using Person = MockShop.Shared.DAO.Person;

namespace MockShop.Server.Logic;

public static class DefaultData
{
    public static async Task Populate(this SampleContext context)
    {
        var personFaker = new Faker<Person>()
            .RuleFor(person => person.FirstName, faker => faker.Name.FirstName())
            .RuleFor(person => person.LastName, faker => faker.Name.LastName())
            .RuleFor(person => person.Age, faker => faker.Random.Int(18, 100))
            .RuleFor(person => person.Gender, faker => faker.Person.Gender.ToString())
            .RuleFor(person => person.Address, faker => faker.Address.FullAddress());
        context.SetupEntities(personFaker, out var persons, 200);
        var productFaker = new Faker<Product>()
            .RuleFor(product => product.Name, faker => faker.Commerce.ProductName())
            .RuleFor(product => product.Price, faker => faker.Random.Decimal(50, 10000));
        context.SetupEntities(productFaker, out var products, 300);
        var orderFaker = new Faker<Order>()
            .RuleFor(order => order.PersonId, faker => faker.PickRandom(persons.Select(person => person.Id)))
            .RuleFor(order => order.Info, faker => faker.Lorem.Sentences());
        context.SetupEntities(orderFaker, out var orders);
        var orderDetailsFaker = new Faker<OrderDetails>()
            .RuleFor(details => details.OrderId, faker => faker
                .PickRandom(orders.Select(order => order.Id)))
            .RuleFor(details => details.ShippingAddress, faker => faker.Address.FullAddress());
        context.SetupEntities(orderDetailsFaker, out _);
        var orderProductsFaker = new Faker<OrderProduct>()
            .RuleFor(orderProduct => orderProduct.Quantity,
                faker => faker.PickRandom(1, 50))
            .RuleFor(orderProduct => orderProduct.OrderId,
                faker => faker.PickRandom(orders.Select(order => order.Id)))
            .RuleFor(orderProduct => orderProduct.ProductId,
                faker => faker.PickRandom(products.Select(product => product.Id)));
        context.SetupEntities(orderProductsFaker, out var orderProducts, 300, false);
        var orderProductsDb = context.OrderProduct.ToList();
        orderProducts = orderProducts.Except(orderProductsDb).ToList();
        orderProducts = orderProducts
            .DistinctBy(orderProduct => new {orderProduct.OrderId, orderProduct.ProductId}).ToList();
        await context.AddRangeAsync(orderProducts);
        await context.SaveChangesAsync();
    }

    private static void SetupEntities<T>(this DbContext context, Faker<T> faker,
        out List<T> entities, int count = 100, bool addToContext = true)
        where T : EntityWithId
    {
        entities = faker.Generate(count);
        var id = context.Set<T>().Any() ? context.Set<T>().Max(entityWithId => entityWithId.Id) : 0;
        entities.ForEach(entityWithId => entityWithId.Id = ++id);
        if (addToContext) context.Set<T>().AddRangeAsync(entities);
    }
}