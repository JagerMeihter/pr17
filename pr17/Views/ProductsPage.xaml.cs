using pr17.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using pr17.Models;
namespace pr17
{
    public partial class ProductsPage : Page
    {
        private List<Product> allProducts = new List<Product>();

        public ProductsPage() 
        {
            InitializeComponent();
            LoadTestProducts();
            DisplayProducts(allProducts);
        }

        private void LoadTestProducts()
        {
            allProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Крем для лица Cosmic Glow", Price = 1890, Discount = 25, Manufacturer = "Lunar Beauty", ProductType = "Уход за лицом", Description = "Интенсивное увлажнение", AverageRating = 4.8f },
                new Product { Id = 2, Name = "Маска ночная Star Dust", Price = 1290, Discount = 10, Manufacturer = "Nebula", ProductType = "Маски", Description = "Восстановление за ночь", AverageRating = 4.5f },
                new Product { Id = 3, Name = "Тушь для ресниц Galaxy Lash", Price = 890, Discount = 30, Manufacturer = "AstroCosmetics", ProductType = "Макияж", Description = "Экстремальный объём", AverageRating = 4.9f },
                new Product { Id = 4, Name = "Шампунь Moon Light", Price = 750, Discount = 5, Manufacturer = "Lunar Beauty", ProductType = "Волосы", Description = "Для повреждённых волос", AverageRating = 4.2f },
                new Product { Id = 5, Name = "Помада Velvet Nebula", Price = 1100, Discount = 18, Manufacturer = "Nebula", ProductType = "Губы", Description = "Матовый финиш", AverageRating = 4.7f },
                new Product { Id = 6, Name = "Сыворотка Anti-Age Orion", Price = 2450, Discount = 22, Manufacturer = "Orion Lab", ProductType = "Уход за лицом", Description = "Мощный anti-age эффект", AverageRating = 4.6f }
            };
        }

        private void DisplayProducts(List<Product> products)
        {
            wrapPanelProducts.Children.Clear();

            foreach (var product in products)
            {
                var card = CreateProductCard(product);
                wrapPanelProducts.Children.Add(card);
            }
        }

        private Border CreateProductCard(Product product)
        {
            var converter = (DiscountConverter)FindResource("DiscountConverter");
            var bgBrush = converter.Convert(product.Discount, null, null, null) as Brush;

            var border = new Border
            {
                Width = 220,
                Margin = new Thickness(10),
                Background = bgBrush,
                CornerRadius = new CornerRadius(8),
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1)
            };

            var stack = new StackPanel { Margin = new Thickness(12) };

            stack.Children.Add(new TextBlock
            {
                Text = product.Name,
                FontWeight = FontWeights.Bold,
                FontSize = 15,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            });

            var pricePanel = new StackPanel { Orientation = Orientation.Horizontal };
            pricePanel.Children.Add(new TextBlock
            {
                Text = product.Price.ToString("0") + " ₽",
                FontSize = 16,
                Foreground = Brushes.White,
                Margin = new Thickness(0, 5, 10, 0)
            });

            if (product.Discount > 0)
            {
                pricePanel.Children.Add(new TextBlock
                {
                    Text = $"-{product.Discount}%",
                    FontSize = 14,
                    Foreground = Brushes.Yellow,
                    VerticalAlignment = VerticalAlignment.Center
                });
            }
            stack.Children.Add(pricePanel);

            stack.Children.Add(new TextBlock
            {
                Text = $"⭐ {product.AverageRating:F1}",
                Foreground = Brushes.Gold,
                Margin = new Thickness(0, 5, 0, 10)
            });

            var btnAdd = new Button
            {
                Content = "В корзину",
                Height = 32,
                Background = new SolidColorBrush(Color.FromRgb(39, 174, 96)),
                Foreground = Brushes.White,
                Margin = new Thickness(0, 8, 0, 0)
            };
            btnAdd.Click += (s, e) =>
            {
                CartService.Add(product);
                MessageBox.Show($"{product.Name} добавлен в корзину!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            };

            stack.Children.Add(btnAdd);
            border.Child = stack;

            return border;
        }

        // === СОБЫТИЯ (важно: имена должны точно совпадать с XAML) ===
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }

        private void ApplyFilter()
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            var filtered = allProducts.FindAll(p =>
                string.IsNullOrEmpty(searchText) ||
                p.Name.ToLower().Contains(searchText) ||
                p.Manufacturer.ToLower().Contains(searchText) ||
                p.ProductType.ToLower().Contains(searchText)
            );

            DisplayProducts(filtered);
        }
    }
}