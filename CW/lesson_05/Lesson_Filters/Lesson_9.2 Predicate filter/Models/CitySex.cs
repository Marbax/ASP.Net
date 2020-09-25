namespace Lesson_9._2_Predicate_filter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CitySex")]
    public partial class CitySex
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(5)]
        public string Sex { get; set; }
    }
}
