using InternetShop.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.BLL.Models.UIModels
{
    public class GoodEditVM
    {
        public GoodEditVM() { }

        public GoodEditVM(Good good, IEnumerable<Category> cats, IEnumerable<Manufacturer> mans) :
            this(good.GoodId, good.GoodName, good.Price, good.GoodCount, good.CategoryId, good.ManufacturerId, cats, mans)
        { }

        public GoodEditVM(int id, string name, decimal price, decimal count, int? catId, int? manId, IEnumerable<Category> cats, IEnumerable<Manufacturer> mans, IEnumerable<HttpPostedFileBase> photos = null)
        {
            GoodId = id;
            GoodName = name;
            GoodPrice = price;
            GoodCount = count;
            CategoryId = catId;
            ManufacturerId = manId;
            Categories = new SelectList(cats, "CategoryId", "CategoryName", CategoryId);
            Manufacturers = new SelectList(mans, "ManufacturerId", "ManufacturerName", ManufacturerId);
            Photos = photos;
        }

        [HiddenInput(DisplayValue = false)]
        public int GoodId { get; set; }

        [Required(ErrorMessage = "Please enter a good name")]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string GoodName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal GoodPrice { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive count")]
        [Display(Name = "Count")]
        public decimal GoodCount { get; set; }

        public int? CategoryId { get; set; }
        public SelectList Categories { get; set; }

        public int? ManufacturerId { get; set; }
        public SelectList Manufacturers { get; set; }

        public IEnumerable<HttpPostedFileBase> Photos { get; set; }
    }

}