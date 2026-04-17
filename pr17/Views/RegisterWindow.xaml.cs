using pr17.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pr17
{
    public partial class RegisterWindow : Window
    {
        public string RegisteredLogin { get; private set; }
        public string RegisteredPassword { get; private set; }

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Логин и пароль обязательны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Login == txtLogin.Text.Trim()))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var newUser = new User
                    {
                        FullName = txtFullName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Login = txtLogin.Text.Trim(),
                        PasswordHash = AuthService.HashPassword(txtPassword.Password),
                        Role = UserRole.Client,        // ←←← Важно: всегда Client
                        IsActive = true
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    RegisteredLogin = newUser.Login;
                    RegisteredPassword = txtPassword.Password;

                    MessageBox.Show("Регистрация прошла успешно!\nВы автоматически вошли в систему.",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.DialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
