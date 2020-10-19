using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.BLL.Models.UIModels
{
    public class ManufacturerVM
    {
        [HiddenInput(DisplayValue = false)]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Please enter a manufacturer name")]
        [StringLength(20)]
        public string ManufacturerName { get; set; }

        public IEnumerable<int> Goods { get; set; } = new List<int>();

    }
}