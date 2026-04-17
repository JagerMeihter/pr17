using pr17.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pr17
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadAllUsers();
        }

        private void LoadAllUsers()
        {
            lvUsers.Items.Clear();

            using (var db = new AppDbContext())
            {
                var users = db.Users.ToList();
                foreach (var user in users)
                {
                    lvUsers.Items.Add(user);
                }
            }
        }

        private void BtnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is User user)
            {
                // Простое переключение роли для теста (в будущем можно сделать выбор)
                user.Role = user.Role == UserRole.Client ? UserRole.Master : UserRole.Client;

                using (var db = new AppDbContext())
                {
                    var u = db.Users.Find(user.Id);
                    if (u != null) u.Role = user.Role;
                    db.SaveChanges();
                }

                LoadAllUsers();
                MessageBox.Show($"Роль пользователя {user.FullName} изменена.", "Успех");
            }
        }

        private void BtnToggleActive_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is User user)
            {
                user.IsActive = !user.IsActive;

                using (var db = new AppDbContext())
                {
                    var u = db.Users.Find(user.Id);
                    if (u != null) u.IsActive = user.IsActive;
                    db.SaveChanges();
                }

                LoadAllUsers();
                MessageBox.Show($"Пользователь {user.FullName} {(user.IsActive ? "разморожен" : "заморожен")}.");
            }
        }
    }
}