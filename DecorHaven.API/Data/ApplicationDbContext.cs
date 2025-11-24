using Microsoft.EntityFrameworkCore;
using DecorHaven.API.Models;

namespace DecorHaven.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Newsletter> Newsletters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Product configuration
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // CartItem configuration
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // WishlistItem configuration
        modelBuilder.Entity<WishlistItem>()
            .HasOne(wi => wi.User)
            .WithMany(u => u.WishlistItems)
            .HasForeignKey(wi => wi.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WishlistItem>()
            .HasOne(wi => wi.Product)
            .WithMany(p => p.WishlistItems)
            .HasForeignKey(wi => wi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order configuration
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // OrderItem configuration
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Review configuration
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Newsletter configuration
        modelBuilder.Entity<Newsletter>()
            .HasIndex(n => n.Email)
            .IsUnique();

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Furniture", Description = "Modern & Classic Pieces", Slug = "furniture", IconClass = "fas fa-couch", CreatedAt = DateTime.UtcNow },
            new Category { Id = 2, Name = "Lighting", Description = "Illuminate Your Space", Slug = "lighting", IconClass = "fas fa-lightbulb", CreatedAt = DateTime.UtcNow },
            new Category { Id = 3, Name = "Wall Art", Description = "Express Your Style", Slug = "wall-art", IconClass = "fas fa-palette", CreatedAt = DateTime.UtcNow },
            new Category { Id = 4, Name = "Accessories", Description = "Perfect Finishing Touches", Slug = "accessories", IconClass = "fas fa-gift", CreatedAt = DateTime.UtcNow }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Modern Velvet Armchair", Description = "Elegant velvet armchair with modern design", Price = 599, OldPrice = 799, CategoryId = 1, IconClass = "fas fa-chair", StockQuantity = 25, AverageRating = 4.5, ReviewCount = 128, IsNew = true, IsFeatured = true, CreatedAt = DateTime.UtcNow },
            new Product { Id = 2, Name = "Crystal Table Lamp", Description = "Beautiful crystal table lamp for ambient lighting", Price = 249, OldPrice = 349, CategoryId = 2, IconClass = "fas fa-lamp", StockQuantity = 40, AverageRating = 5, ReviewCount = 95, IsFeatured = true, CreatedAt = DateTime.UtcNow },
            new Product { Id = 3, Name = "Abstract Canvas Set", Description = "Set of 3 abstract canvas paintings", Price = 189, CategoryId = 3, IconClass = "fas fa-image", StockQuantity = 30, AverageRating = 4, ReviewCount = 67, IsNew = true, CreatedAt = DateTime.UtcNow },
            new Product { Id = 4, Name = "Ceramic Vase Collection", Description = "Handcrafted ceramic vases set", Price = 129, OldPrice = 179, CategoryId = 4, IconClass = "fas fa-vase", StockQuantity = 50, AverageRating = 5, ReviewCount = 142, IsFeatured = true, CreatedAt = DateTime.UtcNow },
            new Product { Id = 5, Name = "Luxury Bed Frame", Description = "King size luxury bed frame with storage", Price = 1299, OldPrice = 1599, CategoryId = 1, IconClass = "fas fa-bed", StockQuantity = 15, AverageRating = 4.5, ReviewCount = 203, IsFeatured = true, CreatedAt = DateTime.UtcNow },
            new Product { Id = 6, Name = "Modern Chandelier", Description = "Contemporary chandelier for dining room", Price = 899, CategoryId = 2, IconClass = "fas fa-chandelier", StockQuantity = 20, AverageRating = 5, ReviewCount = 87, CreatedAt = DateTime.UtcNow },
            new Product { Id = 7, Name = "Gold Mirror Frame", Description = "Decorative gold-framed wall mirror", Price = 349, OldPrice = 449, CategoryId = 3, IconClass = "fas fa-frame", StockQuantity = 35, AverageRating = 4, ReviewCount = 54, CreatedAt = DateTime.UtcNow },
            new Product { Id = 8, Name = "Luxury Candle Set", Description = "Premium scented candle collection", Price = 79, CategoryId = 4, IconClass = "fas fa-candle", StockQuantity = 100, AverageRating = 5, ReviewCount = 176, IsNew = true, IsFeatured = true, CreatedAt = DateTime.UtcNow }
        );
    }
}

