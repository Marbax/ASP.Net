using System.Collections.Generic;

namespace InternetShop.BLL.Models.UIModels
{
    public class CategoriesListVM
    {
        public IEnumerable<CategoryVM> Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}