using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Data;

public class OrderDbContext:DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options):base(options)
    {
        
    }
    public DbSet<ApplicationCore.Entities.Order> Orders { get; set; }
    public DbSet<Order_Details> Order_Details { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order_Details>(ConfigureOrderDetails);
        modelBuilder.Entity<ApplicationCore.Entities.Order>(ConfigureOrder);
    }

    private void ConfigureOrder(EntityTypeBuilder<ApplicationCore.Entities.Order> modelBuilder)
    {
        modelBuilder.HasKey(x => x.Id);
        
    }

    private void ConfigureOrderDetails(EntityTypeBuilder<Order_Details> modelBuilder)
    {
        modelBuilder.HasKey(x => x.Id);
        modelBuilder.HasOne(x=>x.Order)
            .WithMany(x=>x.Order_Details)
            .HasForeignKey(x=>x.Order_Id);
    }
}
