using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        static List<Student> students = Student.GetStudents();

        [HttpGet]
        public List<Student> Get()
        {
            return students;
        }

        public Student Get(int id)
        {
            return students.FirstOrDefault(x => x.Id == id);
        }

        public Student Get(string name)
        {
            return students.FirstOrDefault(x => x.Name == name);
        }

        public Student Get(string name, string lastname)
        {
            return students.FirstOrDefault(x => x.Name == name && x.Lastname == lastname);
        }

        public void Post([FromBody]Student student)
        {
            student.Id = students.Max(x => x.Id) + 1;
            students.Add(student);
        }

        public void Put(int id, [FromBody]Student student)
        {
            Student current = students.FirstOrDefault(x => x.Id == id);
            current.Name = student.Name;
            current.Lastname = student.Lastname;
        }

        public void Delete(int id)
        {
            students.Remove(students.FirstOrDefault(x => x.Id == id));
        }

    }
}
