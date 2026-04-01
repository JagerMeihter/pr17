using pr17;
using pr17.Services;
using System.Windows;

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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}