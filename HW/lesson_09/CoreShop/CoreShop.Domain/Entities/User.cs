using System.ComponentModel.DataAnnotations;

namespace CoreShop.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a login")]
        [StringLength(20)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(20)]
        public string Password { get; set; }

        [DataType(DataType.MultilineText)]
        public string Token { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a lastname")]
        [StringLength(20)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please enter a role")]
        [StringLength(20)]
        public string Role { get; set; }
    }
}
