using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TailorManagementApp.Models;
using TailorManagementApp.Models.Base;
using TailorManagementApp.Models.InventoryModel;
using TailorManagementApp.Models.PurchaseModel;
using TailorManagementApp.Models.RentModel;
using TailorManagementApp.Models.SalesModule;

namespace TailorShopWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<CategoryMeasurement> Enrollments { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetalMeasurement> OrderDetalMeasurements { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentDetail> RentDetails { get; set; }
        public DbSet<RentReturn> RentReturns { get; set; }
        public DbSet<RentReturnDetail> RentReturnDetails { get; set; }
        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<CategoryMeasurement>().ToTable("Enrollment");
            modelBuilder.Entity<Measurement>().ToTable("Measurement");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Purchase>().ToTable("Purchase");
            modelBuilder.Entity<PurchaseDetail>().ToTable("PurchaseDetail");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<OrderDetalMeasurement>().ToTable("OrderDetalMeasurement");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Stock>().ToTable("Stock");
            modelBuilder.Entity<Sales>().ToTable("Sale");
            modelBuilder.Entity<SalesDetail>().ToTable("SalesDetail");
            modelBuilder.Entity<Expense>().ToTable("Expense");
            modelBuilder.Entity<Income>().ToTable("Income");
            modelBuilder.Entity<Rent>().ToTable("Rent");
            modelBuilder.Entity<RentDetail>().ToTable("RentDetail");
            modelBuilder.Entity<RentReturn>().ToTable("RentReturn");
            modelBuilder.Entity<Staff>().ToTable("Staff");

            modelBuilder.Entity<CategoryMeasurement>()
               .HasKey(c => new { c.CategoryID, c.MeasurementID });
            modelBuilder.Entity<OrderDetalMeasurement>()
               .HasKey(c => new { c.OrderDetailID, c.MeasurementID });
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }


    }
}
