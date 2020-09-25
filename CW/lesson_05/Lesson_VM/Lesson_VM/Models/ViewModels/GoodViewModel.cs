using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lesson_VM.Models.ViewModels
{
    public class GoodViewModel
    {
        public string GoodName { get; set; }

        public decimal Price { get; set; }

        public decimal GoodCount { get; set; }
        public int CategoryId { get; set; }

        public SelectList Categories { get; set; }
    }
}