using pr17.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; } = 1;
}
namespace pr17.Models
{
    public enum UserRole { Client, Master, Manager, Administrator }
    public enum AppointmentStatus { Scheduled, Completed, Cancelled }
    public enum PaymentMethod { Cash, Card }

    [Table("Users")]
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = "";

        public string Phone { get; set; } = "";

        [Required]
        public string Login { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public decimal Discount { get; set; } = 0;
        public string Manufacturer { get; set; } = "";
        public string ProductType { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public float AverageRating { get; set; } = 0;
    }

    [Table("ServiceTypes")]
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal BasePrice { get; set; }
    }

    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public virtual User Client { get; set; }

        public int MasterId { get; set; }
        public virtual User Master { get; set; }

        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        public DateTime DateTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Comment { get; set; } = "";
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    }

    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public virtual User Client { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? DeliveryDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsDelivered { get; set; } = false;

        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; } = 1;
        public decimal PriceAtOrder { get; set; }
    }
}