﻿using InternetShop.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InternetShop.BLL.Models.ApiModels
{
    public class GoodApiVM
    {
        private readonly string _imgDir = $"https://localhost:44395/Upload/";

        public GoodApiVM(Good good)
        {
            if (good != null)
            {
                GoodId = good.GoodId;
                GoodName = good.GoodName;
                ManufacturerId = good?.ManufacturerId;
                CategoryId = good?.CategoryId;
                Price = good.Price;
                GoodCount = good.GoodCount;
                if (good.Photos != null && good.Photos.Count > 0)
                    Parallel.ForEach(good.Photos, (g) => Photos.Add($"{_imgDir}{g.PhotoPath}"));
            }
        }

        public int GoodId { get; set; }

        [Required(ErrorMessage = "Please enter a good name")]
        [StringLength(100)]
        public string GoodName { get; set; }

        public int? ManufacturerId { get; set; }

        public int? CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive count")]
        public decimal GoodCount { get; set; }

        public virtual ICollection<string> Photos { get; set; } = new List<string>();
    }

}