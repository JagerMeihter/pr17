using System.Data.Entity;
using pr17.Models;

namespace pr17
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=SqlConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ServiceType>().ToTable("ServiceTypes");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");

            // === ИСПРАВЛЕНИЕ ЦИКЛИЧЕСКОЙ ССЫЛКИ ===

            // Связь Client → Appointment
            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Client)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.ClientId)
                .WillCascadeOnDelete(false);   // Отключаем каскад

            // Связь Master → Appointment
            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Master)
                .WithMany()                    // Мастер не имеет коллекции Appointments в модели
                .HasForeignKey(a => a.MasterId)
                .WillCascadeOnDelete(false);   // Отключаем каскад

            // Связь Client → Order
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Client)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ClientId)
                .WillCascadeOnDelete(false);
        }
    }
}