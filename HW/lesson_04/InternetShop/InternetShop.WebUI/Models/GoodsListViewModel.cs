using InternetShop.Domain.Abstract;
using InternetShop.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.WebUI.Models
{
    public class GoodsListViewModel
    {
        public IEnumerable<Good> Goods { get; set; }
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();

        [Display(Name = "Category")]
        public string CurrentCategory { get; set; } = "";

        [Display(Name = "Manufacturer")]
        public string CurrentManufacturer { get; set; } = "";
        public Filter Filter { get; set; } = new Filter();

        public GoodsListViewModel Page(int page)
        {
            PagingInfo.CurrentPage = page;
            return this;
        }

        public static GoodsListViewModel GetDefaultView() => new GoodsListViewModel() { PagingInfo = new PagingInfo() { CurrentPage = 1 }, Filter = new Filter() };
    }
}
public class Filter
{

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
    [Display(Name = "From")]
    public decimal From { get; set; } = default;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Please enter a positive price")]
    [Display(Name = "To")]
    public decimal To { get; set; } = decimal.MaxValue;
}