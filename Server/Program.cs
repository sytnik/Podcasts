var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
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
application.Run();