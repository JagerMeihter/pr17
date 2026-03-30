using pr17.Services;
using pr17.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationService.MainFrame = MainFrame;

            // Запуск с главной страницы
            MainFrame.Navigate(new StartPage());

            UpdateTopPanel();
        }

        // Обновление верхней панели в зависимости от роли
        private void UpdateTopPanel()
        {
            bool isLoggedIn = AuthService.CurrentUser != null;

            btnCart.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
            btnAccount.Visibility = (isLoggedIn && AuthService.CurrentUser.Role == UserRole.Client)
                                  ? Visibility.Visible
                                  : Visibility.Collapsed;

            btnCart.Content = $"Корзина ({CartService.Items.Count})";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsPage());
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            CartService.Clear();
            UpdateTopPanel();
            NavigationService.Navigate(new StartPage());
        }

        // Метод для вызова из других окон после логина
        public void RefreshUI()
        {
            UpdateTopPanel();
        }
    }
}