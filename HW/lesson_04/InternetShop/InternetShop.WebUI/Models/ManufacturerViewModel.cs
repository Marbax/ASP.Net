using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.WebUI.Models
{
    public class ManufacturerViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Please enter a manufacturer name")]
        [StringLength(20)]
        public string ManufacturerName { get; set; }
    }
}