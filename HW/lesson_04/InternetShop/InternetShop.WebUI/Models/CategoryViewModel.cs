using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.WebUI.Models
{
    public class CategoryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        [StringLength(20)]
        public string CategoryName { get; set; }
    }
}