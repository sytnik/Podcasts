using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options =>
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/home/login");
builder.Services.AddControllersWithViews();
var application = builder.Build();
if (!application.Environment.IsDevelopment())
{
    application.UseExceptionHandler("/Home/Error");
    application.UseHsts();
}
application.UseHttpsRedirection();
application.UseStaticFiles();
application.UseRouting();
application.UseAuthentication();
application.UseAuthorization();
application.MapControllerRoute(name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
application.Run();

namespace MvcProj
{
    public partial class Program
    {
    
    }
}