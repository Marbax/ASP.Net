using System;

namespace InternetShop.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 1;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}