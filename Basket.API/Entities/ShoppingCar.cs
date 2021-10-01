using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCar
    {
        public string UserName { get; set; }
        public List<ShoppingCarItem> Items { get; set; } = new List<ShoppingCarItem>();

        public ShoppingCar()
        {
        }

        public ShoppingCar(string name)
        {
            UserName = name;
        }

        public decimal TotalPrice 
        {
            get 
            {
                decimal price = 0;
                foreach (ShoppingCarItem item in Items)
                {
                    price += item.Price * item.Quantity;
                }
                return price;
            }
        }
    }
}
