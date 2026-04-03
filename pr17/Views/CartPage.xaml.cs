using pr17.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using pr17.Models;
namespace pr17
{
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            stackCartItems.Children.Clear();

            if (CartService.Items.Count == 0)
            {
                var emptyText = new TextBlock
                {
                    Text = "Корзина пуста",
                    FontSize = 18,
                    Foreground = System.Windows.Media.Brushes.Gray,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 50, 0, 0)
                };
                stackCartItems.Children.Add(emptyText);
                txtTotal.Text = "Итого: 0 ₽";
                return;
            }

            decimal total = 0;

            foreach (var item in CartService.Items)
            {
                var card = CreateCartItemCard(item);
                stackCartItems.Children.Add(card);

                total += item.Product.Price * item.Quantity;
            }

            txtTotal.Text = $"Итого: {total:0} ₽";
        }

        private Border CreateCartItemCard(CartItem item)
        {
            var border = new Border
            {
                Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(44, 62, 80)),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 8, 0, 8),
                Padding = new Thickness(15)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // Название и цена
            var infoStack = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
            infoStack.Children.Add(new TextBlock
            {
                Text = item.Product.Name,
                FontWeight = FontWeights.Bold,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 15
            });
            infoStack.Children.Add(new TextBlock
            {
                Text = $"{item.Product.Price:0} ₽ × {item.Quantity}",
                Foreground = System.Windows.Media.Brushes.LightGray
            });

            Grid.SetColumn(infoStack, 0);
            grid.Children.Add(infoStack);

            // Кнопки количества
            var qtyPanel = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };

            var btnMinus = new Button { Content = "-", Width = 30, Height = 30, Margin = new Thickness(5) };
            var btnPlus = new Button { Content = "+", Width = 30, Height = 30, Margin = new Thickness(5) };
            var txtQty = new TextBlock
            {
                Text = item.Quantity.ToString(),
                Width = 30,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 16
            };

            btnMinus.Click += (s, e) => ChangeQuantity(item, -1);
            btnPlus.Click += (s, e) => ChangeQuantity(item, 1);

            qtyPanel.Children.Add(btnMinus);
            qtyPanel.Children.Add(txtQty);
            qtyPanel.Children.Add(btnPlus);

            Grid.SetColumn(qtyPanel, 1);
            grid.Children.Add(qtyPanel);

            // Кнопка удаления
            var btnRemove = new Button
            {
                Content = "✕",
                Width = 40,
                Height = 40,
                Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(231, 76, 60)),
                Foreground = System.Windows.Media.Brushes.White,
                Margin = new Thickness(10, 0, 0, 0)
            };
            btnRemove.Click += (s, e) => RemoveItem(item);

            Grid.SetColumn(btnRemove, 2);
            grid.Children.Add(btnRemove);

            border.Child = grid;
            return border;
        }

        private void ChangeQuantity(CartItem item, int delta)
        {
            item.Quantity += delta;
            if (item.Quantity < 1) item.Quantity = 1;
            LoadCart();
        }

        private void RemoveItem(CartItem item)
        {
            CartService.Remove(item);
            LoadCart();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Очистить всю корзину?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CartService.Clear();
                LoadCart();
            }
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (CartService.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var checkoutWindow = new CheckoutWindow();
            if (checkoutWindow.ShowDialog() == true)
            {
                // Здесь можно добавить создание заказа
                MessageBox.Show("Заказ успешно оформлен!\nСпасибо за покупку в Космической Ложе.",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                CartService.Clear();
                LoadCart();
                NavigationService.Navigate(new StartPage());
            }
        }
    }
}