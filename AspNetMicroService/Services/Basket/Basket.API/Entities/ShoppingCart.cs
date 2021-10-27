using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }

        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    totalPrice += item.Quantity * item.Price;
                }

                return totalPrice;
            }
        }

    }
}
