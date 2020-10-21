using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreShop.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter a category name")]
        [StringLength(20)]
        public string CategoryName { get; set; }
        public virtual ICollection<Good> Good { get; set; }
    }
}