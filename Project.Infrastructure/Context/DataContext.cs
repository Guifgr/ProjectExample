using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Group = System.Text.RegularExpressions.Group;

namespace Project.Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        modelBuilder.Entity<User>(entity => entity.Property(e => e.Guid).HasDefaultValueSql("gen_random_uuid()"));
    }

    public DbSet<User> Users { get; set; }

}
