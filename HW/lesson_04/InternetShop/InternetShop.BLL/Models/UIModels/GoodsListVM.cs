using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.BLL.Models.UIModels
{
    public class GoodsListVM
    {
        public IEnumerable<GoodVM> Goods { get; set; } = new List<GoodVM>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();

        [Display(Name = "Category")]
        public string CurrentCategory { get; set; } = "";

        [Display(Name = "Manufacturer")]
        public string CurrentManufacturer { get; set; } = "";
        public Filter Filter { get; set; } = new Filter();
    }
}
