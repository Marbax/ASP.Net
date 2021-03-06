﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_9._2_Predicate_filter.Models
{
	public interface IGenericRepository<T>

	{
		IEnumerable<T> GetAll();
		IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
		T Get(int id);
		void AddOrUpdate(T obj);
		void Delete(T obj);
	}
}
