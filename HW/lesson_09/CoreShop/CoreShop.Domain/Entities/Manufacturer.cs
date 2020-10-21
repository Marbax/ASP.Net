using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreShop.Domain.Entities
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        [Required(ErrorMessage = "Please enter a manufacturer name")]
        [StringLength(20)]
        public string ManufacturerName { get; set; }
        public virtual ICollection<Good> Good { get; set; }
    }
}