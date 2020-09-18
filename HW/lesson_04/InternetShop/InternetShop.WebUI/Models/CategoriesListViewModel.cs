using InternetShop.Domain.Entities;
using System.Collections.Generic;

namespace InternetShop.WebUI.Models
{
    public class CategoriesListViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}