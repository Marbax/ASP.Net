using InternetShop.Domain.Entities;
using System.Collections.Generic;

namespace InternetShop.WebUI.Models
{
    public class GoodsListViewModel
    {
        public IEnumerable<Good> Goods { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentManufacturer { get; set; }
    }
}