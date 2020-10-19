using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.BLL.Models.UIModels
{
    public class GoodVM
    {
        public GoodVM() { }

        [HiddenInput(DisplayValue = false)]
        public int GoodId { get; set; }

        [Required(ErrorMessage = "Please enter a good name")]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string GoodName { get; set; } = "";

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal GoodPrice { get; set; } = 1;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive count")]
        [Display(Name = "Count")]
        public decimal GoodCount { get; set; } = 1;

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public IEnumerable<CategoryVM> Categories { get; set; } = new List<CategoryVM>();
        [Display(Name = "Select Category")]
        public SelectList SelectCategories { get => new SelectList(Categories, "CategoryId", "CategoryName", CategoryId); }

        public int? ManufacturerId { get; set; }
        public string ManufacturerName { get; set; } = "";
        public IEnumerable<ManufacturerVM> Manufacturers { get; set; } = new List<ManufacturerVM>();
        [Display(Name = "Select Manufacturer")]
        public SelectList SelectManufacturers { get => new SelectList(Manufacturers, "ManufacturerId", "ManufacturerName", ManufacturerId); }

        public IEnumerable<string> Photos { get; set; }

        public IEnumerable<HttpPostedFileBase> UploadedPhotos { get; set; } = new List<HttpPostedFileBase>();

    }
}