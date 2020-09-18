using InternetShop.Domain.Entities;
using System.Collections.Generic;

namespace InternetShop.WebUI.Models
{
    public class GoodEditViewModel
    {
        public Good Good { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
    }
}