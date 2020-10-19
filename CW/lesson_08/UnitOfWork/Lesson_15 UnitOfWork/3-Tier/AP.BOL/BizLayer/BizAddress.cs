using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.BOL.BizLayer
{
    public class BizAddress
    {
        public int AddressId { get; set; }

        public int StreetId { get; set; }
        [Required]
        [StringLength(128)]
        public string StreetName { get; set; }


        public int SubdivisionId { get; set; }
        [Required]
        [StringLength(64)]
        public string SubdivisionName { get; set; }

        [Required]
        [StringLength(12)]
        public string House { get; set; }

        [StringLength(24)]
        public string Serial { get; set; }

        public int? СountFloor { get; set; }

        public int? СountEntrance { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
