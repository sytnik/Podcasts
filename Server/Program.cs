using Microsoft.EntityFrameworkCore;
using MockShop.Server.DAO;
using MockShop.Server.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SampleContext>(optionsBuilder =>
    optionsBuilder.UseInMemoryDatabase("MockShop"));
builder.Services.AddScoped<ShopRepository>();
var application = builder.Build();
if (application.Environment.IsDevelopment())
{
    application.UseWebAssemblyDebugging();
}
else
{
    application.UseExceptionHandler("/Error");
    application.UseHsts();
}

application.UseHttpsRedirection();
application.UseBlazorFrameworkFiles();
application.UseStaticFiles();
application.UseRouting();
application.MapRazorPages();
application.MapControllers();
application.MapFallbackToFile("index.html");
await application.Services.CreateAsyncScope()
    .ServiceProvider.GetService<SampleContext>().Populate();
application.MapGet("persons", async (ShopRepository repository) =>
    await repository.Persons());
application.Run();