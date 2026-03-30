using System;
using System.Collections.Generic;





namespace pr17.Services
{


    public enum UserRole { Client, Master, Manager, Administrator }
    public enum AppointmentStatus { Scheduled, Completed, Cancelled }
    public enum PaymentMethod { Cash, Card }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Login { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public decimal Discount { get; set; } // в процентах
        public string Manufacturer { get; set; } = "";
        public string ProductType { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public float AverageRating { get; set; } = 0;
    }

    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal BasePrice { get; set; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public User Client { get; set; }
        public int MasterId { get; set; }
        public User Master { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime DateTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Comment { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    }

    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public User Client { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? DeliveryDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsDelivered { get; set; } = false;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal PriceAtOrder { get; set; }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;
    }
}