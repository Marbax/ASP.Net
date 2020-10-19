using System.Collections.Generic;

namespace InternetShop.BLL.Models.UIModels
{
    public class ManufacturersListVM
    {
        public IEnumerable<ManufacturerVM> Manufacturers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}