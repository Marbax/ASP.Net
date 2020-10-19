using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.BLL.Models.UIModels
{
    public class PhotoVM
    {
        [HiddenInput(DisplayValue = false)]
        public int PhotoId { get; set; }

        public int? GoodId { get; set; }

        [Required]
        [StringLength(200)]
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "There should be an image format file.")]
        public string PhotoPath { get; set; }
    }

}