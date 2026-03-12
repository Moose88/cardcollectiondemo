using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsDemo.Models
{
    public class Cards
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }
        public string Quality { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BinderId { get; set; } // For assigning cards to a binder.
    }
}