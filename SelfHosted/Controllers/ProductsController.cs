using SelfHosted.Controllers;
using SelfHosted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SelfHosted.Controllers
{
    public class ProductsController : RavenController
    {
        //Product[] products = new Product[]  
        //{  
        //    new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },  
        //    new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },  
        //    new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }  
        //};

        public IEnumerable<Product> Get()
        {
            //var p1 = new SelfHosted.Model.Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 };
            //var p2 = new SelfHosted.Model.Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M };
            //var p3 = new SelfHosted.Model.Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M };

            //Db.Store(p1);
            //Db.Store(p2);
            //Db.Store(p3);
            //Db.SaveChanges();

            return Db.Query<Product>();
        }

        public Product Get(int id)
        {
            var p = Db.Load<Product>(id);
            if (p == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return p;
        }
    }
}
