using pr17.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using pr17.Models;
namespace pr17
{
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
            LoadTestData();
        }

        private void LoadTestData()
        {
            // Тестовые записи
            lvAppointments.Items.Clear();
            var testAppointments = new[]
            {
                new
                {
                    Client = new User { FullName = "Дрель ВИТАЛЯ" },
                    Master = new User { FullName = "Трубоёб ВАСИЛИЙ" },
                    ServiceType = new ServiceType { Name = "Маникюр" },
                    DateTime = DateTime.Now.AddHours(4)
                },
                new
                {
                    Client = new User { FullName = "Хуяковна Хераковна" },
                    Master = new User { FullName = "Трубоёб ВАСИЛИЙ" },
                    ServiceType = new ServiceType { Name = "Окрашивание" },
                    DateTime = DateTime.Now.AddHours(7)
                }
            };

            foreach (var app in testAppointments)
            {
                lvAppointments.Items.Add(app);
            }

            // Тестовые заказы
            lvOrders.Items.Clear();
            var testOrders = new[]
            {
                new { Client = new User { FullName = "Дрель ВИТАЛЯ" }, OrderDate = DateTime.Now.AddDays(-2) },
                new { Client = new User { FullName = "ГИТЛЕР ГИТЛЕР" }, OrderDate = DateTime.Now.AddDays(-5) }
            };

            foreach (var order in testOrders)
            {
                lvOrders.Items.Add(order);
            }
        }

        private void BtnNewAppointment_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция создания новой записи будет доступна после подключения базы данных.",
                "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnDeliverOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                MessageBox.Show("Заказ успешно выдан клиенту!",
                    "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}