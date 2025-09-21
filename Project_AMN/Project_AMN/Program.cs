using Project_AMN.Handlers;

var builder = WebApplication.CreateBuilder(args);

// HttpClient
builder.Services.AddHttpClient();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateArticleHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateArticleHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateUserHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateUserHandler).Assembly);
});

// Blazor Components
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Authentication & Authorization
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

// Only allow Admins to access certain endpoints
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

// Full Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // ingen mailbekräftelse
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

// Database
var connectionString = "Data Source=ProjectAMN.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Tjänster
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInboundService, InboundService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IUserService, UserService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Map Endpoints
app.MapOrderEndpoints();
app.MapInboundEndpoints();
app.MapArticleEndpoints();
app.MapAdminEndpoints();

// Razor Components
app.MapRazorComponents<App>()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Project_AMN.Client._Imports).Assembly);


// Identity endpoints
app.MapAdditionalIdentityEndpoints();

// Skapa roller och admin
await InitializeRolesAndAdmin(app);

app.Run();


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
