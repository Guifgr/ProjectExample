using BRW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Group = System.Text.RegularExpressions.Group;

namespace Project.Infrastructure.Context;

public class BrwAppContext: DbContext
{
    public BrwAppContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrwAppContext).Assembly);
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Guid).HasDefaultValueSql("uuid()");
        });
    }

    public DbSet<User> Users { get; set; }

}
