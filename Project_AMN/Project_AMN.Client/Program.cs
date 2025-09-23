using Project_AMN.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project_AMN.Client.Services;
using Project_AMN.Client.ApiServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

/// <summary>
/// Adds support for authorization services in Blazor WebAssembly.
/// </summary>
builder.Services.AddAuthorizationCore();

/// <summary>
/// Adds support for cascading authentication state across the application.
/// </summary>
builder.Services.AddCascadingAuthenticationState();

/// <summary>
/// Registers a persistent authentication state provider for handling authentication.
/// </summary>
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

/// <summary>
/// Registers a scoped HttpClient with the base address set to the host environment.
/// </summary>
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

/// <summary>
/// Registers the AdminService for handling user administration.
/// </summary>
builder.Services.AddScoped<AdminService>();

/// <summary>
/// Registers the ArticleService for handling article operations.
/// </summary>
builder.Services.AddScoped<ArticleService>();

/// <summary>
/// Registers the OrderService for handling orders.
/// </summary>
builder.Services.AddScoped<OrderService>();

/// <summary>
/// Registers the InboundService for managing inbound items.
/// </summary>
builder.Services.AddScoped<InboundService>();

/// <summary>
/// Builds and runs the Blazor WebAssembly application.
/// </summary>
await builder.Build().RunAsync();
