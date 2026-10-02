using Microsoft.EntityFrameworkCore;
using MiniPuanApi.Models;

namespace MiniPuanApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<PointTransaction> PointTransactions => Set<PointTransaction>();
}