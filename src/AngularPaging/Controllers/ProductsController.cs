using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNet.Mvc;

namespace AngularPaging.Controllers
{
    public class ProductsController : Controller
    {
        private const int COUNT = 21748;

        // GET: api/products
        [HttpGet]
        [Route("api/products/{order}/{sort}/{skip:int}/{take:int}")]
        public Page Get(string order, string sort, int skip, int take)
        {
            return GetPage(order, sort, skip, take);
        }

        public Page GetPage(string order, string sort, int skip, int take)
        {            
            return new Page()
            {
                Products = GetProducts().OrderBy($"{order} {sort.ToUpper()}").Skip(skip).Take(take).ToList(),
                Count = COUNT
            };
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();
            for (var i = 0; i < COUNT; i++)
            {
                var random = new Randomizer();
                var lorem = new Lorem();
                products.Add(new Product
                {
                    Id = i + 1,
                    Title = lorem.Sentence(),
                    Price = random.Number(1, 100)
                });
            }

            return products;
        }
    }

    public class Page
    {
        public IList<Product> Products { get; set; }
        public int Count { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}