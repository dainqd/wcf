using baseNetApi.models;
using Microsoft.EntityFrameworkCore;

namespace baseNetApi.context;

public class MySQLDBContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Contacts> Contacts { get; set; }
    public DbSet<Groups> Groups { get; set; }
    
    public MySQLDBContext(DbContextOptions<MySQLDBContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new DbInitializer(modelBuilder).Seed();
    }
}