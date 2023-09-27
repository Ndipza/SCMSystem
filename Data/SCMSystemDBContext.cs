using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Data
{
    public class SCMSystemDBContext : DbContext
    {
        public SCMSystemDBContext(DbContextOptions<SCMSystemDBContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<ArticleHashTag>()
            //    .HasKey(sma => new { sma.ArticleId, sma.HashTagId });

            //modelBuilder.Entity<ArticleHashTag>()
            //    .HasOne(sma => sma.Article)
            //    .WithMany(sma => sma.ArticleHashTags)
            //    .HasForeignKey(s => s.ArticleId);

            //modelBuilder.Entity<ArticleHashTag>()
            //    .HasOne(sma => sma.HashTag)
            //    .WithMany(sma => sma.ArticleHashTags)
            //    .HasForeignKey(s => s.HashTagId);

            //modelBuilder.Entity<ArticleFeature>()
            //    .HasKey(sma => new { sma.ArticleId, sma.FeatureId });

            //modelBuilder.Entity<ArticleFeature>()
            //    .HasOne(sma => sma.Article)
            //    .WithMany(sma => sma.ArticleFeatures)
            //    .HasForeignKey(s => s.ArticleId);

            //modelBuilder.Entity<ArticleFeature>()
            //    .HasOne(sma => sma.Feature)
            //    .WithMany(sma => sma.ArticleFeatures)
            //    .HasForeignKey(s => s.FeatureId);

            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.Articles)
            //    .WithOne(a => a.Category)
            //    .IsRequired();

            //modelBuilder.Entity<AppUser>()
            //.HasOne(user => user.UserRole);

            //modelBuilder.Entity<Role>();
        }

    }
}