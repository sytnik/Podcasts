using BlazorApp;
using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpContextAccessor();
// var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<SomeDataContextOnConfiguring>();
builder.Services.AddScoped(provider =>
    provider.GetService<IDbContextFactory<SomeDataContextOnConfiguring>>().CreateDbContext());
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();