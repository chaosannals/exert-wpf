using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq.Expressions;

namespace CSServerDemo;

public class MainDbContextDesignFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var opb = new DbContextOptionsBuilder<MainDbContext>();
        opb.UseMySQL(builder.Configuration.GetConnectionString("Main"));
        return new MainDbContext(opb.Options);
    }
}
