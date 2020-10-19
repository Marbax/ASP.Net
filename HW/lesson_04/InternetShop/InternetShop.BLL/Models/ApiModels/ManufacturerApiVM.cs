using InternetShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetShop.BLL.Models.ApiModels
{
    public class ManufacturerApiVM
    {
        public ManufacturerApiVM(Manufacturer man)
        {
            ManufacturerId = man.ManufacturerId;
            ManufacturerName = man.ManufacturerName;
            if (man.Good != null && man.Good.Count > 0)
                Parallel.ForEach(man.Good, (g) => Goods.Add(g.GoodId));
        }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public List<int> Goods { get; set; } = new List<int>();
    }

}