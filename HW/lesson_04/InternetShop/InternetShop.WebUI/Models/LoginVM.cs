using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.WebUI.Models
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; } = "Home/Index";
    }
}