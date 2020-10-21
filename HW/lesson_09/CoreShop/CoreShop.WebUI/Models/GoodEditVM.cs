using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreShop.WebUI.Models
{
    public class GoodEditVM
    {
        public GoodEditVM() { }
        public GoodEditVM(Good good)
        {
            GoodId = good.GoodId;
            GoodName = good.GoodName;
            Price = good.Price;
            GoodCount = good.GoodCount;
            CategoryId = good.CategoryId;
            ManufacturerId = good.ManufacturerId;
        }
        public Good Good { get => new Good() { GoodId = this.GoodId, GoodName = this.GoodName, Price = this.Price, GoodCount = this.GoodCount, ManufacturerId = this.ManufacturerId, CategoryId = this.CategoryId }; }
        public int GoodId { get; set; }
        [Required]
        [StringLength(20)]
        public string GoodName { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "79228162514264337593543950335")]
        public decimal GoodCount { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public SelectList SelectCategory { get => new SelectList(Categories, nameof(CategoryId), "CategoryName", CategoryId); }
        public int? ManufacturerId { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public SelectList SelectManufacturer { get => new SelectList(Manufacturers, nameof(ManufacturerId), "ManufacturerName", ManufacturerId); }

    }
}