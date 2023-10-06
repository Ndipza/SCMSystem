using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SCMSystemDBContext : IdentityDbContext<IdentityUser>
    {
        public SCMSystemDBContext(DbContextOptions<SCMSystemDBContext> options) : base(options)
        {

        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartStatus> CartStatuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<Product> Products { get; set; }

        //public DbSet<User> Users { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<CartStatus>()
                .HasMany(c => c.Carts)
                .WithOne(p => p.CartStatus)
                .OnDelete(DeleteBehavior.SetNull);


            builder.Entity<PaymentStatus>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.PaymentStatus)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<PaymentMethod>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.PaymentMethod)
                .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<CustomerStatus>()
            //    .HasMany(c => c.Customers)
            //    .WithOne(p => p.CustomerStatus)
            //    .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<UserRole>()
            //   .HasMany(c => c.Users)
            //   .WithOne(p => p.UserRole)
            //   .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Customer>()
            //   .HasMany(c => c.Carts)
            //   .WithOne(p => p.Customer)
            //   .OnDelete(DeleteBehavior.SetNull);           

            //modelBuilder.Entity<User>()
            //   .HasMany(c => c.Carts)
            //   .WithOne(p => p.User)
            //   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Product>()
               .HasMany(c => c.CartItems)
               .WithOne(p => p.Product)
               .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Cart>()
               .HasMany(c => c.CartItems)
               .WithOne(p => p.Cart)
               .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Cart>()
               .HasMany(c => c.Payments)
               .WithOne(p => p.Cart)
               .OnDelete(DeleteBehavior.SetNull);
            base.OnModelCreating(builder);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>()
        //        .HasMany(c => c.Products)
        //        .WithOne(p => p.Category)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    modelBuilder.Entity<CartStatus>()
        //        .HasMany(c => c.Carts)
        //        .WithOne(p => p.CartStatus)
        //        .OnDelete(DeleteBehavior.SetNull);


        //    modelBuilder.Entity<PaymentStatus>()
        //        .HasMany(c => c.Payments)
        //        .WithOne(p => p.PaymentStatus)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    modelBuilder.Entity<PaymentMethod>()
        //        .HasMany(c => c.Payments)
        //        .WithOne(p => p.PaymentMethod)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    //modelBuilder.Entity<CustomerStatus>()
        //    //    .HasMany(c => c.Customers)
        //    //    .WithOne(p => p.CustomerStatus)
        //    //    .OnDelete(DeleteBehavior.SetNull);

        //    //modelBuilder.Entity<UserRole>()
        //    //   .HasMany(c => c.Users)
        //    //   .WithOne(p => p.UserRole)
        //    //   .OnDelete(DeleteBehavior.SetNull);

        //    //modelBuilder.Entity<Customer>()
        //    //   .HasMany(c => c.Carts)
        //    //   .WithOne(p => p.Customer)
        //    //   .OnDelete(DeleteBehavior.SetNull);           

        //    //modelBuilder.Entity<User>()
        //    //   .HasMany(c => c.Carts)
        //    //   .WithOne(p => p.User)
        //    //   .OnDelete(DeleteBehavior.SetNull);

        //    modelBuilder.Entity<Product>()
        //       .HasMany(c => c.CartItems)
        //       .WithOne(p => p.Product)
        //       .OnDelete(DeleteBehavior.SetNull);

        //    modelBuilder.Entity<Cart>()
        //       .HasMany(c => c.CartItems)
        //       .WithOne(p => p.Cart)
        //       .OnDelete(DeleteBehavior.SetNull);

        //    modelBuilder.Entity<Cart>()
        //       .HasMany(c => c.Payments)
        //       .WithOne(p => p.Cart)
        //       .OnDelete(DeleteBehavior.SetNull);
           
        //}
        
    }
}