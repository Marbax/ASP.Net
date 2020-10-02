using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.DbLayer
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        //[UIHint("Proms")]
        //public EmpPromotion proms { get; set; }
    }


    public class EmployeeMetadata
    {
        //[DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}", ApplyFormatInEditMode =true)]
        [UIHint("Date1")]
        public DateTime DateBirthday { get; set; }
    }
}
