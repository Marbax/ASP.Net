using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.DbLayer
{
    [MetadataType(typeof(JobTitleMetadata))]
    public partial class JobTitle
    {

    }


    public class JobTitleMetadata
    {
        [Required(ErrorMessage = "Поле обязательно к заполнению!")]
        [Display(Name ="Наименование должности")]
        public string NameJobTitle { get; set; }
    }
}
