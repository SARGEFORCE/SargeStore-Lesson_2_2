using System.Collections.Generic;
using System.Linq;

namespace SargeStoreDomain.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemCount => Items?.Sum(item => item.Quantity) ?? 0;
    }
}
