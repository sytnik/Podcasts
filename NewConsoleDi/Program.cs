var applicationBuilder = Host.CreateApplicationBuilder();
applicationBuilder.Logging.SetMinimumLevel(LogLevel.Information);
var mapperConfiguration = new MapperConfiguration(expression => expression.AddProfile<MappingProfile>());
var instance = mapperConfiguration.CreateMapper();
instance.ConfigurationProvider.AssertConfigurationIsValid();
applicationBuilder.Services.AddSingleton(instance);
applicationBuilder.Services.AddDbContext<ShopContext>(builder => builder.UseSqlServer(
        applicationBuilder.Configuration.GetConnectionString("AppDb"),
        contextOptionsBuilder => contextOptionsBuilder
            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
var host = applicationBuilder.Build();
var mapper = host.Services.GetService<IMapper>();
await using var shopContext = host.Services.GetService<ShopContext>();

// await MockTestData(shopContext);

// var customersWithOrdersAndProductsIncluded = await GetCustomersWithOrdersAndProductsIncluded(shopContext);
// var dtoFromCustomers = GetDtoFromCustomers(customersWithOrdersAndProductsIncluded);
// var mapDtoFromCustomers = mapper.MapDtoFromCustomers(customersWithOrdersAndProductsIncluded);

// var projectDtoWithRelated = await ProjectDtoWithRelated(shopContext);

var projectDtoWithRelatedAutoMapper = await mapper.ProjectDtoWithRelatedAutoMapper(shopContext);

Console.ReadKey();
applicationBuilder.Build().Run();