public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
     public DbSet<Order> Orders { get; set; }
    public DbSet<Article> Articles{ get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    foreach (var property in entityType.GetProperties())
                    {
                        if (property.GetColumnType() == "nvarchar(max)")
                        {
                            property.SetColumnType("TEXT");
                        }
                    }
                }
            }
        }
}
