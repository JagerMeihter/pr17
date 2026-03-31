using pr17.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace pr17
{
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();
            LoadUserInfo();
            LoadAppointments();
            LoadOrders();
        }

        private void LoadUserInfo()
        {
            if (AuthService.CurrentUser != null)
            {
                txtFullName.Text = AuthService.CurrentUser.FullName;
                txtPhone.Text = AuthService.CurrentUser.Phone;
            }
        }

        private void LoadAppointments()
        {
            lvAppointments.Items.Clear();

            if (AuthService.CurrentUser == null) return;

            // Пока тестовые данные (в будущем будем брать из базы)
            var testAppointments = new[]
            {
                new { ServiceType = new ServiceType { Name = "Маникюр" },
                      Master = new User { FullName = "Анна Звёздная" },
                      DateTime = DateTime.Now.AddDays(2),
                      Status = "Запланирована" },

                new { ServiceType = new ServiceType { Name = "Окрашивание волос" },
                      Master = new User { FullName = "Анна Звёздная" },
                      DateTime = DateTime.Now.AddDays(5),
                      Status = "Запланирована" }
            };

            foreach (var app in testAppointments)
            {
                lvAppointments.Items.Add(app);
            }
        }

        private void LoadOrders()
        {
            lvOrders.Items.Clear();

            if (AuthService.CurrentUser == null) return;

            // Тестовые заказы
            var testOrders = new[]
            {
                new { OrderDate = DateTime.Now.AddDays(-3), DeliveryDate = DateTime.Now.AddDays(1) },
                new { OrderDate = DateTime.Now.AddDays(-10), DeliveryDate = DateTime.Now.AddDays(-5) }
            };

            foreach (var order in testOrders)
            {
                lvOrders.Items.Add(order);
            }
        }
    }
}