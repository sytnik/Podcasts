namespace MvcProj.IntegrationTests;

public class Tests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public Tests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Theory]
    [InlineData("/")]
    [InlineData("/home/index")]
    public async Task GetPageAllowAnonymous(string url)
    {
        // arrange
        var client = _factory
            .CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});
        // act
        var response = await client.GetAsync(url);
        // assert
        response.EnsureSuccessStatusCode();
        var contentType = "text/html; charset=utf-8";
        Assert.Equal(contentType,
            response.Content.Headers.ContentType?.ToString());
    }

    [Theory]
    [InlineData("/home/privacy")]
    [InlineData("/home/SomeTaskForManager")]
    public async Task GetRedirectWithNoAuth(string url)
    {
        // arrange
        var client = _factory
            .CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});
        // act
        var response = await client.GetAsync(url);
        // assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        var loginPath = "/home/login";
        Assert.Contains(loginPath, response.Headers.Location?.OriginalString);
    }

    [Theory]
    [InlineData("/home/privacy")]
    public async Task GetPageForAdmin(string url) => await GetPageForRole<AdminAuthHandler>(url);

    [Theory]
    [InlineData("/home/SomeTaskForManager")]
    public async Task GetPageForManager(string url) => await GetPageForRole<ManagerAuthHandler>(url);

    private async Task GetPageForRole<T>(string url)
        where T : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        // arrange
        var hostBuilder = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(collection => collection.AddAuthentication(defaultScheme: "TestScheme")
                .AddScheme<AuthenticationSchemeOptions, T>("TestScheme", _ => { }));
        });
        var client = hostBuilder
            .CreateClient(new WebApplicationFactoryClientOptions {AllowAutoRedirect = false});
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "TestScheme");
        // act
        var response = await client.GetAsync(url);
        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

public class AdminAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public AdminAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() =>
        AuthTestExtensions.HandleAuthenticateForRole("Admin");
}

public class ManagerAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public ManagerAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() =>
        AuthTestExtensions.HandleAuthenticateForRole("Manager");
}

public static class AuthTestExtensions
{
    public static Task<AuthenticateResult> HandleAuthenticateForRole(string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, $"Test {role}"),
            new(ClaimsIdentity.DefaultRoleClaimType, role)
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}