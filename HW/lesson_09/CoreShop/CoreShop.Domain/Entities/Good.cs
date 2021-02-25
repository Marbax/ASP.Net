using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreShop.Domain.Entities
{
    public class Good
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}