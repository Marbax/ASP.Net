using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson_9._2_Predicate_filter.Models
{
	public class GoodsViewModel
	{
		public IEnumerable<Good> Goods { get; set; }
		public IList<PriceFilter> Filters { get; set; }
	}
}