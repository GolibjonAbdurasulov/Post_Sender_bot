using Microsoft.EntityFrameworkCore;
using Post_Sender_bot.Domain.Entitys;

namespace Post_Sender_bot.Infrastructure;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Post { get; set; }
    public DataContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
        optionsBuilder.UseNpgsql("Host=213.230.65.55; Port=5444; Database=postgres; username=postgres; password=159357Dax;");
        base.OnConfiguring(optionsBuilder);
    }
}