using System;
using System.Collections.Generic;

namespace BibliotekConsole.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
