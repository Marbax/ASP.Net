using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProductRepository :IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int _nextId = 1;

        public ProductRepository()
        {
            Add(new Product { Name = "Tomato soup", Category = "Groceries", Price = 1.39M });
            Add(new Product { Name = "Yo-yo", Category = "Toys", Price = 3.75M });
            Add(new Product { Name = "Hammer", Category = "Hardware", Price = 16.99M });
            Add(new Product { Name = "Network Cards", Category = "Electronics", Price = 6.59M });
            Add(new Product { Name = "Spotting Scopes", Category = "Optics", Price = 25.99M });
            Add(new Product { Name = "Biometric Monitors", Category = "Health Care", Price = 100.0M });
            Add(new Product { Name = "Perfume", Category = "Cosmetics", Price = 10.99M });
            Add(new Product { Name = "Hair Coloring", Category = "Personal Care", Price = 16.99M });
        }
        public Product Add(Product item)
        {
            item.Id = _nextId++;
            products.Add(item);
            return item;
        }
        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product Get(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Product item)
        {
            int index = products.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            products.Add(item);
            return true;
        }

    }
}