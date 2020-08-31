using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaginationDemo.Models
{
    public class Student
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public static IEnumerable<Student> GetStudents()
        {
            return new List<Student>()
            {
                new Student{ Name="Stud_01",LastName="Last_01"},
                new Student{ Name="Stud_02",LastName="Last_02"},
                new Student{ Name="Stud_03",LastName="Last_03"},
                new Student{ Name="Stud_04",LastName="Last_04"},
                new Student{ Name="Stud_05",LastName="Last_05"},
                new Student{ Name="Stud_06",LastName="Last_06"},
                new Student{ Name="Stud_07",LastName="Last_07"},
                new Student{ Name="Stud_08",LastName="Last_08"},
                new Student{ Name="Stud_09",LastName="Last_09"},
                new Student{ Name="Stud_10",LastName="Last_10"},
                new Student{ Name="Stud_11",LastName="Last_11"},
                new Student{ Name="Stud_12",LastName="Last_12"},
                new Student{ Name="Stud_13",LastName="Last_13"},
                new Student{ Name="Stud_14",LastName="Last_14"}
            };
        }
    }
}