using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Domain.Entities
{
    [Table("Photo")]
    public partial class Photo
    {
        public int PhotoId { get; set; }

        [ForeignKey("Good")]
        public int? GoodId { get; set; }

        public virtual Good Good { get; set; }

        [Required]
        [StringLength(200)]
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "There should be an image format file.")]
        public string PhotoPath { get; set; }
    }
}
