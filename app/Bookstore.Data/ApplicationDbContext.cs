using Bookstore.Domain.Addresses;
using Bookstore.Domain.Books;
using Bookstore.Domain.Carts;
using Bookstore.Domain.Customers;
using Bookstore.Domain.Offers;
using Bookstore.Domain.Orders;
using Bookstore.Domain.ReferenceData;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace Bookstore.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Offer> Offer { get; set; }

        public DbSet<ReferenceDataItem> ReferenceData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Get target schema name from SchemaMapper
            string targetSchema = SchemaMapper.GetTargetSchemaName();
            modelBuilder.HasDefaultSchema(targetSchema);
            
            modelBuilder.Entity<Customer>().HasIndex(x => x.Sub)
                .IsUnique()
                .HasDatabaseName("ix_customer_sub");

            // Configure tables with appropriate schema mappings using SchemaMapper
            modelBuilder.Entity<Address>().ToTable(
                SchemaMapper.GetTargetTableName("Address"), targetSchema);
                
            modelBuilder.Entity<Book>().ToTable(
                SchemaMapper.GetTargetTableName("Book"), targetSchema);
                
            modelBuilder.Entity<Customer>().ToTable(
                SchemaMapper.GetTargetTableName("Customer"), targetSchema);
                
            modelBuilder.Entity<Order>().ToTable(
                SchemaMapper.GetTargetTableName("Order"), targetSchema);
                
            modelBuilder.Entity<ShoppingCart>().ToTable(
                SchemaMapper.GetTargetTableName("ShoppingCart"), targetSchema);
                
            modelBuilder.Entity<ShoppingCartItem>().ToTable(
                SchemaMapper.GetTargetTableName("ShoppingCartItem"), targetSchema);
                
            modelBuilder.Entity<OrderItem>().ToTable(
                SchemaMapper.GetTargetTableName("OrderItem"), targetSchema);
                
            modelBuilder.Entity<Offer>().ToTable(
                SchemaMapper.GetTargetTableName("Offer"), targetSchema);
                
            modelBuilder.Entity<ReferenceDataItem>().ToTable(
                SchemaMapper.GetTargetTableName("ReferenceData"), targetSchema);

            // Configure boolean properties for PostgreSQL compatibility based on entity class examination
            // From Address.cs: public bool IsActive { get; set; } = true;
            modelBuilder.Entity<Address>().Property(x => x.IsActive).HasConversion<int>();
            
            // From Book.cs: public bool IsInStock => Quantity > 0; and public bool IsLowInStock => Quantity <= LowBookThreshold;
            modelBuilder.Entity<Book>().Property(x => x.IsInStock).HasConversion<int>();
            modelBuilder.Entity<Book>().Property(x => x.IsLowInStock).HasConversion<int>();
            
            // From ShoppingCartItem.cs: public bool WantToBuy { get; set; }
            modelBuilder.Entity<ShoppingCartItem>().Property(x => x.WantToBuy).HasConversion<int>();

            // Configure relationships
            modelBuilder.Entity<Book>().HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>().HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>().HasOne(x => x.Publisher).WithMany().HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.BookType).WithMany().HasForeignKey(x => x.BookTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Offer>().HasOne(x => x.Condition).WithMany().HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().HasOne(x => x.Customer).WithMany().OnDelete(DeleteBehavior.Restrict);

            PopulateDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}