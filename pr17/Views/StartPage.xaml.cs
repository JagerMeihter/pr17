using pr17.Services;
using System.Windows;
using System.Windows.Controls;
using pr17.Models;

namespace pr17
{
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();

            if (loginWindow.ShowDialog() == true)
            {
                // После успешного входа переходим на нужную страницу в зависимости от роли
                if (AuthService.CurrentUser != null)
                {
                    switch (AuthService.CurrentUser.Role)
                    {
                        case UserRole.Client:
                            NavigationService.Navigate(new AccountPage());
                            break;

                        case UserRole.Master:
                            NavigationService.Navigate(new MasterPage());
                            break;

                        case UserRole.Manager:
                            NavigationService.Navigate(new ManagerPage());
                            break;

                        case UserRole.Administrator:
                            NavigationService.Navigate(new AdminPage());
                            break;

                        default:
                            NavigationService.Navigate(new StartPage()); // на всякий случай
                            break;
                    }
                }
            }
        }

        private void BtnShop_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsPage());
        }
    }
}