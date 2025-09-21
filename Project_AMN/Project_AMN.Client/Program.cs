using Project_AMN.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project_AMN.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress
)
});
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<InboundService>();


await builder.Build().RunAsync();


