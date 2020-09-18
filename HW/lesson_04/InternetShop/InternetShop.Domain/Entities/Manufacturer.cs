namespace InternetShop.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Manufacturer")]
    public partial class Manufacturer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manufacturer()
        {
            Good = new HashSet<Good>();
        }

        [HiddenInput(DisplayValue = false)]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Please enter a manufacturer name")]
        [StringLength(20)]
        public string ManufacturerName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Good> Good { get; set; }
    }
}
