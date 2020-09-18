using InternetShop.Domain.Entities;
using System.Collections.Generic;

namespace InternetShop.WebUI.Models
{
    public class ManufacturersListViewModel
    {
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}