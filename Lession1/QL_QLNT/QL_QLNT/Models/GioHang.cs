using System.Collections.Generic;
using System.Linq;

namespace Web_QLNT.Models
{
    public class GioHang
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddToCart(SanPham product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(item => item.SanPham.MaSP == product.MaSP);
            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
                Items.Add(new CartItem { SanPham = product, Quantity = quantity });
        }

        public int GetTotalQuantity() => Items.Sum(item => item.Quantity);
        public decimal GetTotalPrice() => Items.Sum(item => item.SanPham.DonGia * item.Quantity);

        public void RemoveFromCart(string productId)
        {
            var cartItem = Items.FirstOrDefault(item => item.SanPham.MaSP == productId);
            if (cartItem != null) Items.Remove(cartItem);
        }

        public void ClearCart() => Items.Clear();
    }

    public class CartItem
    {
        public SanPham SanPham { get; set; }
        public int Quantity { get; set; }
    }
}