using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_AMN.Client.Pages;
using Project_AMN.Components;
using Project_AMN.Components.Account;
using Project_AMN.Data;
using Project_AMN.ApiRoutes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();


builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

builder.Services.AddAuthorization();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


var connectionString = "Data Source=ProjectAMN.db"; 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.MapApiRoutes();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseSwagger();       
app.UseSwaggerUI();    

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Project_AMN.Client._Imports).Assembly);

// Identity endpoints
app.MapAdditionalIdentityEndpoints();

// Skapa roller och admin vid uppstart
await InitializeRolesAndAdmin(app);

app.Run();


// --- Metoden för att initiera roller och admin ---
async Task InitializeRolesAndAdmin(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        string email = "admin@admin.se";
        string password = "Abc123!";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
