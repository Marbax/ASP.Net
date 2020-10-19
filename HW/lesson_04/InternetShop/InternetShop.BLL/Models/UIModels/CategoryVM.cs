using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.BLL.Models.UIModels
{
    public class CategoryVM
    {
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        [StringLength(20)]
        public string CategoryName { get; set; }

        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }
}