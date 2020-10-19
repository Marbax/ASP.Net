using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.BLL.Models.UIModels
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = "user";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "password";

        [HiddenInput]
        public string ReturnUrl { get; set; } = "Home/Index";
    }
}