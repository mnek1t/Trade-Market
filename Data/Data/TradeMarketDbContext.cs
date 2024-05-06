using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data.Data
{
    public class TradeMarketDbContext : DbContext
    {
        public TradeMarketDbContext(DbContextOptions<TradeMarketDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptDetail> ReceiptsDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Surname).HasColumnName("Surname");
                entity.Property(e => e.BirthDate);
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PersonId).IsRequired();
                entity.Property(e => e.DiscountValue).IsRequired();
                entity.HasOne(e => e.Person)
                    .WithOne()
                    .HasForeignKey<Customer>(e => e.PersonId)
                    .OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.OperationDate).IsRequired();
                entity.Property(e => e.IsCheckedOut).IsRequired();

                entity.HasOne(d => d.Customer)
                        .WithMany(p => p.Receipts)
                        .HasForeignKey(e => e.CustomerId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Receipt_Customer");
            });
            modelBuilder.Entity<ReceiptDetail>(entity =>
            {
                entity.HasKey(rd => rd.Id);
                entity.Property(rd => rd.ReceiptId).IsRequired();
                entity.Property(rd => rd.ProductId).IsRequired();
                entity.Property(rd => rd.DiscountUnitPrice).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(rd => rd.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(rd => rd.Quantity).IsRequired();

                entity.HasOne(d => d.Receipt)
                        .WithMany(p => p.ReceiptDetails)
                        .HasForeignKey(e => e.ReceiptId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_ReceiptDetail_Receipt");
                entity.HasOne(d => d.Product)
                        .WithMany(p =>p.ReceiptDetails)
                        .HasForeignKey(e => e.ProductId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_ReceiptDetail_Product");
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.ProductCategoryId).IsRequired();
                entity.Property(p => p.ProductName);
                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Category)
                        .WithMany(pc => pc.Products)
                        .HasForeignKey(e => e.ProductCategoryId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(pc => pc.Id);
                entity.Property(pc => pc.CategoryName);
                
            });
        }
    }
}
