namespace InternetShop.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Good")]
    public partial class Good
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Good()
        {
            SalePos = new HashSet<SalePos>();
            Photos = new HashSet<Photo>();
        }

        [HiddenInput(DisplayValue = false)]
        public int GoodId { get; set; }

        [Required(ErrorMessage = "Please enter a good name")]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string GoodName { get; set; }

        public int? ManufacturerId { get; set; }

        public int? CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive count")]
        [Display(Name = "Count")]
        public decimal GoodCount { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePos> SalePos { get; set; }
    }
}
