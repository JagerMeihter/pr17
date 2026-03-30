using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace pr17.Services
{
    public static class AuthService
    {
        public static User CurrentUser { get; private set; }

        private static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public static bool Login(string login, string password)
        {
            // Тестовые пользователи (в реальности можно хранить в List<User>)
            var users = GetTestUsers();
            foreach (var u in users)
            {
                if (u.Login == login && u.PasswordHash == HashPassword(password) && u.IsActive)
                {
                    CurrentUser = u;
                    return true;
                }
            }
            return false;
        }

        public static void Logout() { CurrentUser = null; }

        private static List<User> GetTestUsers()
        {
            return new List<User>
        {
            new User
            {
                Id = 1, FullName = "Администратор", Phone = "+79991234567", Login = "admin",
                PasswordHash = HashPassword("admin123"), Role = UserRole.Administrator
            },
            new User
            {
                Id = 2, FullName = "Анна Звёздная", Phone = "+79991112233", Login = "master",
                PasswordHash = HashPassword("master123"), Role = UserRole.Master
            },
            new User
            {
                Id = 3, FullName = "Иван Клиент", Phone = "+79995556677", Login = "client",
                PasswordHash = HashPassword("client123"), Role = UserRole.Client
            }
        };
        }
    }
}