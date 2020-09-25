using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson_9._2_Predicate_filter.Models
{
	public class PriceFilter
	{
		public int From { get; set; }
		public int To { get; set; }
		public bool IsChecked { get; set; }
	}
}