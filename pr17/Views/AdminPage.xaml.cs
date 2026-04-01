using pr17.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace pr17
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            lvUsers.Items.Clear();

            // Тестовые пользователи
            var testUsers = new[]
            {
                new User
                {
                    Id = 1,
                    FullName = "Администратор",
                    Login = "admin",
                    Phone = "+7 (999) 123-45-67",
                    Role = UserRole.Administrator,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    FullName = "Трубоёб ВАСИЛИЙ",
                    Login = "master",
                    Phone = "+7 (999) 111-22-33",
                    Role = UserRole.Master,
                    IsActive = true
                },
                new User
                {
                    Id = 3,
                    FullName = "Дрель ВИТАЛЯ",
                    Login = "client",
                    Phone = "+7 (999) 555-66-77",
                    Role = UserRole.Client,
                    IsActive = true
                },
                new User
                {
                    Id = 4,
                    FullName = "Гитлер Гитлер",
                    Login = "manager",
                    Phone = "+7 (999) 777-88-99",
                    Role = UserRole.Manager,
                    IsActive = false
                }
            };

            foreach (var user in testUsers)
            {
                lvUsers.Items.Add(user);
            }
        }

        private void BtnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is User user)
            {
                MessageBox.Show($"Изменение роли пользователя:\n{user.FullName}\n\n" +
                               $"Текущая роль: {user.Role}\n\n" +
                               "Функция смены роли будет доступна после подключения базы данных.",
                    "Смена роли", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnToggleActive_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is User user)
            {
                string action = user.IsActive ? "заморожен" : "разморожен";
                MessageBox.Show($"Пользователь {user.FullName} успешно {action}.",
                    "Статус пользователя", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}