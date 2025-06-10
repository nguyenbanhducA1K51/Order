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
    public DbSet<Payment_Method> Payment_Methods { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment_Type> Payment_Types { get; set; }
    public DbSet<User_Address> User_Address { get; set; }
    public DbSet<Shopping_Cart_Item> Shopping_Cart_Items { get; set; }
    public DbSet<Shopping_Cart>ShoppingCarts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order_Details>(ConfigureOrderDetails);
        modelBuilder.Entity<ApplicationCore.Entities.Order>(ConfigureOrder);
    }

    private void ConfigureOrderDetails(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);
    }

    private void ConfigureOrder(EntityTypeBuilder<User_Address> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.HasOne(ua => ua.Address)
            .WithMany(a => a.User_Address)
            .HasPrincipalKey(a => a.Id);
        builder.HasOne(ua=>ua.Customer)
            .WithMany(c => c.UserAddresses)
            .HasPrincipalKey(c => c.Id);

    }

    private void ConfigureShoppingCardItem(EntityTypeBuilder<Shopping_Cart_Item> builder)
    {
        
    }

    private void ConfigureAddresses(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(address => address.Id);
        
    }

    private void ConfigureShoppingCartItems(EntityTypeBuilder<Shopping_Cart_Item> builder)
    {
        builder.HasKey(shoppingCart => shoppingCart.Id);
        builder.HasOne(sc=>sc.ShoppingCart)
            .WithMany(sc=>Shopping_Cart_Items)
            .HasForeignKey(sc=>sc.Id);
            
    }

    private void ConfigreShoppingCart(EntityTypeBuilder<Shopping_Cart_Item> builder)
    {
        builder.HasKey(shoppingCart => shoppingCart.Id);
    }
    private void ConfigurePaymentMethods(EntityTypeBuilder<Payment_Method> builder)
    {
        builder.HasKey(paymentMethod => paymentMethod.Id);
        builder.HasOne(pm=>pm.PaymentType)
            .WithMany(p=>p.PaymentMethods)
            .HasForeignKey(paymentMethod=>paymentMethod.Id);
    }

    private void ConfigurePaymentTypes(EntityTypeBuilder<Payment_Type> builder)
    {
        builder.HasKey(paymentType => paymentType.Id);
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
