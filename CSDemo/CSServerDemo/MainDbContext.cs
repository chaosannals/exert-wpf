using CSServerDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CSServerDemo;

public class MainDbContext :DbContext
{
    // DbFirst 需要这个传参的构造函数才能获取 Program.cs 里面的
    public MainDbContext(DbContextOptions<MainDbContext> options): base(options) { }
    public DbSet<UserAccount> UserAccounts { get; set; } = null!;
}
