using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSample.Models
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
                new Student {Id = 1, Name = "Игорь", Lastname = "Пышкин"},
                new Student {Id = 2, Name = "Виталий", Lastname = "Дарцев"},
                new Student {Id = 3, Name = "Александр", Lastname = "Беляев"},
                new Student {Id = 4, Name = "Григорий", Lastname = "Сковорода"},
                new Student {Id = 5, Name = "Иван", Lastname = "Пишта"}
            };
        }
    }
}