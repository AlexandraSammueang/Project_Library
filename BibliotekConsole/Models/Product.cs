using System;
using System.Collections.Generic;

namespace BibliotekConsole.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string? Isbn { get; set; }
        public string? Isan { get; set; }
        public int? CategoriesId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductInfo { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? BookPages { get; set; }
        public bool? EVersion { get; set; }
        public int? StockValue { get; set; }
        public bool? IsBookable { get; set; }
        public int? IsBookedAmount { get; set; }
        public DateTime? BookedTime { get; set; }
        public double? Price { get; set; }

        public virtual Author? Author { get; set; }
        public virtual ProductCategory? Categories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
