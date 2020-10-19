using InternetShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetShop.BLL.Models.ApiModels
{
    public class CategoryApiVM
    {
        public CategoryApiVM(Category cat)
        {
            CategoryId = cat.CategoryId;
            CategoryName = cat.CategoryName;
            if (cat.Good != null && cat.Good.Count > 0)
                Parallel.ForEach(cat.Good, (g) => Goods.Add(g.GoodId));
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<int> Goods { get; set; } = new List<int>();
    }

}