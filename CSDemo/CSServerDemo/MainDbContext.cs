using CSServerDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSServerDemo;

public class MainDbContext :DbContext
{
    // DbFirst 需要这个传参的构造函数才能获取 Program.cs 里面的
    public MainDbContext(DbContextOptions<MainDbContext> options): base(options) { }

    public DbSet<UserAccount> UserAccounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserAccount>()
            .Property(x => x.CreateAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
