using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebBanBanhMi.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        [DisplayName("Món")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Money
        {
            get
            {
                return Quantity * Price;
            }
        }
    }
}