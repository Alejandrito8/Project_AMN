using Project_AMN.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project_AMN.Client.ApiRoutes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5000")
});

builder.Services.AddScoped<AdminRoute>();
builder.Services.AddScoped<ArticleRoute>();
builder.Services.AddScoped<OrderRoute>();
builder.Services.AddScoped<InboundRoute>();

await builder.Build().RunAsync();

