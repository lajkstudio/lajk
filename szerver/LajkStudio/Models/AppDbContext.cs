using Microsoft.EntityFrameworkCore;

namespace LajkStudio.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("cart");
            
            entity.Property(e => e.CreatedAt)
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("cart_item");
            
            entity.Property(e => e.CartId)
                .HasColumnName("cart_id")
                .IsRequired();

            entity.Property(e => e.Img)
                .HasColumnName("img")
                .HasColumnType("BLOB")
                .IsRequired();

            entity.Property(e => e.TotalPrice)
                .HasColumnName("total_price")
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Cart)
                .WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}