using System.Collections.Generic;

namespace pr17.Services
{
    public static class CartService
    {
        public static List<CartItem> Items { get; } = new List<CartItem>();

        public static void Add(Product product)
        {
            var existing = Items.Find(x => x.Product.Id == product.Id);
            if (existing != null)
                existing.Quantity++;
            else
                Items.Add(new CartItem { Product = product, Quantity = 1 });
        }

        public static void Remove(CartItem item)
        {
            Items.Remove(item);
        }

        public static void Clear()
        {
            Items.Clear();
        }
    }
}