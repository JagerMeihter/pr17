using pr17.Models;
using System;
using System.Linq;
using System.Windows;

namespace pr17
{
    public static class AuthService
    {
        public static User CurrentUser { get; private set; }

        /// <summary>
        /// Проверка логина и пароля через базу PostgreSQL
        /// </summary>
        public static bool Login(string login, string password)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == login && u.IsActive);

                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }

                    // Временная простая проверка пароля (для диагностики)
                    if (password == "123" || password == user.Login + "123")
                    {
                        CurrentUser = user;
                        return true;
                    }

                    MessageBox.Show("Неверный пароль.\n\nДля теста используйте:\nadmin123, master123, client123, manager123",
                        "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// Проверка пароля (SHA256)
        /// </summary>
        private static bool VerifyPassword(string password, string storedHash)
        {
            string hashedInput = HashPassword(password);
            return hashedInput == storedHash;
        }

        /// <summary>
        /// Хэширование пароля с помощью SHA256
        /// </summary>
        public static string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Создание тестовых пользователей (вызывать один раз при первом запуске)
        /// </summary>
        public static void SeedTestUsers()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // Если хотя бы один пользователь уже есть — не создаём заново
                    if (db.Users.Any())
                    {
                        return;
                    }

                    var testUsers = new[]
                    {
                new User
                {
                    FullName = "Администратор",
                    Phone = "+7 (999) 123-45-67",
                    Login = "admin",
                    PasswordHash = HashPassword("admin123"),
                    Role = UserRole.Administrator,
                    IsActive = true
                },
                new User
                {
                    FullName = "Анна Звёздная",
                    Phone = "+7 (999) 111-22-33",
                    Login = "master",
                    PasswordHash = HashPassword("master123"),
                    Role = UserRole.Master,
                    IsActive = true
                },
                new User
                {
                    FullName = "Иван Клиент",
                    Phone = "+7 (999) 555-66-77",
                    Login = "client",
                    PasswordHash = HashPassword("client123"),
                    Role = UserRole.Client,
                    IsActive = true
                },
                new User
                {
                    FullName = "Мария Менеджер",
                    Phone = "+7 (999) 777-88-99",
                    Login = "manager",
                    PasswordHash = HashPassword("manager123"),
                    Role = UserRole.Manager,
                    IsActive = true
                }
            };

                    db.Users.AddRange(testUsers);
                    db.SaveChanges();

                    MessageBox.Show("Тестовые пользователи успешно созданы в базе данных!\n\n" +
                                   "Логины и пароли:\n" +
                                   "admin / admin123\n" +
                                   "master / master123\n" +
                                   "client / client123\n" +
                                   "manager / manager123",
                        "База данных инициализирована",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании пользователей:\n{ex.Message}",
                    "Ошибка базы данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}