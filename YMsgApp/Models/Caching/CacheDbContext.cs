using Microsoft.EntityFrameworkCore;
using YMsgApp.Configurations;
using YMsgApp.Models.Caching.CacheModels;

namespace YMsgApp.Models.Caching;

public class CacheDbContext : DbContext
{
    // private static string _dbFileName;
    //
    // public static string DbFileName
    // {
    //     get => _dbFileName;
    //     set => _dbFileName= string.IsNullOrEmpty(_dbFileName)?value:_dbFileName;
    // }

    public DbSet<TokenCache> TokenCaches { get; set; }

    public DbSet<MessageCache> MessageCaches { get; set; }
    
    public DbSet<UserCache> UserCaches { get; set; }
    

    public CacheDbContext(DbContextOptions options) : base(options)
    {
        SQLitePCL.Batteries_V2.Init();

        Database.EnsureCreated();
    }
    
    
    protected override async void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TokenCache>().HasKey(r => r.Id);

        builder.Entity<MessageCache>().HasKey(r => r.Id);

        builder.Entity<UserCache>().HasKey(r => r.Id);
        

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite();
    }
}