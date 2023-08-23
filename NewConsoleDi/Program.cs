using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewConsoleDi;

var provider = ConfigureServices();
await using var context = provider.GetRequiredService<SomeDataContext>();
var array = await GetUserNames(context);
foreach (var name in array) Console.WriteLine(name);
Console.ReadKey();

static ServiceProvider ConfigureServices()
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(basePath: Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    var collection = new ServiceCollection();
    collection.AddDbContext<SomeDataContext>(builder => builder.UseSqlServer(connectionString));
    return collection.BuildServiceProvider();
}

Task<string[]> GetUserNames(SomeDataContext dataContext) =>
    dataContext.Persons.Select(person => $"{person.FirstName} {person.LastName}").ToArrayAsync();