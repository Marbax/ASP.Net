using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Sample.Models
{
    public class Car
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Title { get; set; }
       
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        public static IEnumerable<Car> GetCars()
        {
            return new List<Car>
            {
                new Car {Id = 1, Title = "Honda", Model = "Accord", Year = 2012},
                new Car {Id = 2, Title = "Lexus", Model = "IS250", Year = 2014},
                new Car {Id = 3, Title = "Honda", Model = "Civic", Year = 1998},
                new Car {Id = 4, Title = "Subaru", Model = "Impreza", Year = 2001}

            };
        }
    }
}