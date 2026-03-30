using pr17;
using pr17.Services;
using pr17.Views;
using System.Windows;
using System.Windows.Controls;

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
                // После успешного входа можно сразу перейти на нужную страницу в зависимости от роли
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