using pr17.Services;
using System;
using System.Windows;
using System.Windows.Controls;

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
                    Client = new User { FullName = "Иван Клиент" },
                    Master = new User { FullName = "Анна Звёздная" },
                    ServiceType = new ServiceType { Name = "Маникюр" },
                    DateTime = DateTime.Now.AddHours(4)
                },
                new
                {
                    Client = new User { FullName = "Мария Смирнова" },
                    Master = new User { FullName = "Анна Звёздная" },
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
                new { Client = new User { FullName = "Иван Клиент" }, OrderDate = DateTime.Now.AddDays(-2) },
                new { Client = new User { FullName = "Елена Петрова" }, OrderDate = DateTime.Now.AddDays(-5) }
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