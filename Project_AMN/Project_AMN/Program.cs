using Project_AMN.Handlers;

/// <summary>
/// Main entry point for the application.
/// Configures services, middleware, endpoints, and initializes roles/admin.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Add HttpClient support.
/// </summary>
builder.Services.AddHttpClient();

/// <summary>
/// Configure MediatR handlers.
/// </summary>
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateArticleHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateArticleHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateUserHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateUserHandler).Assembly);
});

/// <summary>
/// Configure Blazor Components.
/// </summary>
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

/// <summary>
/// Configure Authentication & Authorization.
/// </summary>
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

/// <summary>
/// Configure authorization policies.
/// </summary>
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

/// <summary>
/// Configure full ASP.NET Core Identity.
/// </summary>
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // No email confirmation required
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

/// <summary>
/// Configure database.
/// </summary>
var connectionString = "Data Source=ProjectAMN.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

/// <summary>
/// Register application services.
/// </summary>
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInboundService, InboundService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IUserService, UserService>();

/// <summary>
/// Configure Swagger.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();

/// <summary>
/// Configure the HTTP request pipeline.
/// </summary>
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

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

/// <summary>
/// Map API endpoints.
/// </summary>
app.MapOrderEndpoints();
app.MapInboundEndpoints();
app.MapArticleEndpoints();
app.MapAdminEndpoints();

/// <summary>
/// Map Razor Components.
/// </summary>
app.MapRazorComponents<App>()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Project_AMN.Client._Imports).Assembly);

/// <summary>
/// Map additional identity endpoints.
/// </summary>
app.MapAdditionalIdentityEndpoints();

/// <summary>
/// Initialize roles and create admin user if not exists.
/// </summary>
await InitializeRolesAndAdmin(app);

app.Run();

/// <summary>
/// Creates required roles and an admin user if they do not exist.
/// </summary>
async Task InitializeRolesAndAdmin(WebApplication app)
{
    using var scope = app.Services.CreateScope();
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
