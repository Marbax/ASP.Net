using System.ComponentModel.DataAnnotations;

namespace InternetShop.BLL.Models.UIModels
{
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
}