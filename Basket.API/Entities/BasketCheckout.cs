using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class BasketCheckout
    {
        public string UserName {  get; set; }
        public decimal TotalPrice {  get; set; }

    }
}
