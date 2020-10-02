using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson_11._1.WebApiSample.Models
{
	public class Student
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public static List<Student> GetStudents()
		{
			return new List<Student>
			{
				new Student {Id = 1, Name = "Виктор", Lastname = "Сирык"},
				new Student {Id = 2, Name = "Андрей", Lastname = "Мамчур"},
				new Student {Id = 3, Name = "Юрий", Lastname = "Базак"},
				new Student {Id = 4, Name = "Леонид", Lastname = "Листопадов"}
			};
		}
	}
}