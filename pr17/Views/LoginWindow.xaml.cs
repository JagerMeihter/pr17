using pr17;
using pr17.Services;
using System.Windows;
using pr17.Models;
namespace pr17
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("ты чо елан...!", "дебил блять", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (AuthService.Login(txtLogin.Text.Trim(), txtPassword.Password))
            {
                // Обновляем главное окно
                if (Application.Current.MainWindow is MainWindow main)
                {
                    main.RefreshUI();
                }

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWin = new RegisterWindow();
            if (registerWin.ShowDialog() == true)
            {
                // После успешной регистрации автоматически пытаемся войти
                if (AuthService.Login(registerWin.RegisteredLogin, registerWin.RegisteredPassword))
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}