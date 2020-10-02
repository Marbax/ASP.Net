using Lesson_11._1.WebApiSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lesson_11._1.WebApiSample.Controllers
{
	public class StudentsController : ApiController
	{
		static List<Student> students = Student.GetStudents();
		// GET api/<controller>
		public IEnumerable<Student> Get()
		{
			return students;
		}

		// GET api/<controller>/5
		public Student Get(int id)
		{
			return students.FirstOrDefault(x => x.Id == id);
		}

		// POST api/<controller>
		public void Post([FromBody]Student value)
		{
			students.Add(value);
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]Student value)
		{
			Student student = students.FirstOrDefault(x => x.Id == id);
			if (student != null)
			{
				student.Name = value.Name;
				student.Lastname = value.Lastname;
			}
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
			students.Remove(students.FirstOrDefault(x => x.Id == id));
		}
	}
}