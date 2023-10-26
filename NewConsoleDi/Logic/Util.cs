namespace NewConsoleDi.Logic;

public static class Util
{
    public static async Task MockTestData(ShopContext context)
    {
        var shops = new Faker<Shop>()
            .RuleFor(shop => shop.Id, faker => faker.IndexFaker + 1)
            .RuleFor(shop => shop.Name, faker => faker.Company.CompanyName())
            .RuleFor(shop => shop.Address, faker => faker.Address.FullAddress())
            .Generate(50);
        var products = new Faker<Product>()
            .RuleFor(product => product.Id, faker => faker.IndexFaker + 1)
            .RuleFor(product => product.Name, faker => faker.Commerce.ProductName())
            .RuleFor(product => product.Price, faker => faker.Random.Decimal(20, 5000))
            .RuleFor(product => product.ShopId, faker => faker.PickRandom(shops).Id)
            .Generate(500);
        var customers = new Faker<Customer>()
            .RuleFor(customer => customer.Id, faker => faker.IndexFaker + 1)
            .RuleFor(customer => customer.FirstName, faker => faker.Name.FirstName())
            .RuleFor(customer => customer.LastName, faker => faker.Name.LastName())
            .RuleFor(customer => customer.Age, faker => faker.Random.Int(18, 80))
            .RuleFor(customer => customer.Email, faker => faker.Internet.Email())
            .RuleFor(customer => customer.Gender, faker => faker.Person.Gender.ToString())
            .RuleFor(customer => customer.Address, faker => faker.Address.FullAddress())
            .Generate(50);
        var orders = new Faker<Order>()
            .RuleFor(order => order.Id, faker => faker.IndexFaker + 1)
            .RuleFor(order => order.CustomerId, faker => faker.PickRandom(customers).Id)
            .RuleFor(order => order.Date, faker => faker.Date.Past())
            .RuleFor(order => order.Address, faker => faker.Address.FullAddress())
            .Generate(300);
        var orderProducts = new Faker<OrderProduct>()
            .RuleFor(orderProduct => orderProduct.OrderId, faker => faker.PickRandom(orders).Id)
            .RuleFor(orderProduct => orderProduct.ProductId, faker => faker.PickRandom(products).Id)
            .RuleFor(orderProduct => orderProduct.Quantity, faker => faker.Random.Int(1, 10))
            .Generate(2000)
            .DistinctBy(product => new { OrdersId = product.OrderId, product.ProductId }).ToList();
        await context.OverwriteTable(shops);
        await context.OverwriteTable(products);
        await context.OverwriteTable(customers);
        await context.OverwriteTable(orders);
        await context.OverwriteTable(orderProducts);
    }

    private static async Task OverwriteTable<T>(this DbContext context, IEnumerable<T> entities) where T : class
    {
        await context.TruncateAsync<T>();
        await context.BulkInsertAsync(entities);
    }

    public static Task<Customer[]> GetCustomersWithOrdersAndProductsIncluded(ShopContext context) =>
        context.Customer
            .Include(customer => customer.Orders)
            .ThenInclude(orders => orders.Products)
            .ToArrayAsync();

    public static CustomerDto[] GetDtoFromCustomers(IEnumerable<Customer> customers) =>
        customers.Select(customer => new CustomerDto(customer.FirstName, customer.LastName,
                customer.Orders.Select(order => new OrderDto(order.Date,
                    order.Products.Select(product => new ProductDto(product.Name, product.Price))
                        .ToArray()))
                    .ToArray()))
            .ToArray();

    public static CustomerDto[] MapDtoFromCustomers(this IMapper mapper, Customer[] customers) =>
        mapper.Map<CustomerDto[]>(customers);

    public static Task<CustomerDto[]> ProjectDtoWithRelated(ShopContext context) =>
        context.Customer.Select(customer => new CustomerDto(customer.FirstName, customer.LastName,
                customer.Orders.Select(order => new OrderDto(order.Date,
                        order.Products.Select(product => new ProductDto(product.Name, product.Price))
                            .ToArray()))
                    .ToArray()))
            .ToArrayAsync();

    public static Task<CustomerDto[]> ProjectDtoWithRelatedAutoMapper(this IMapper mapper, ShopContext context) =>
        context.Customer.ProjectTo<CustomerDto>(mapper.ConfigurationProvider).ToArrayAsync();
}