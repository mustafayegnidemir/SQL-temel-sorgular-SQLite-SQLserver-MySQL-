using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using consoleApp.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace consoleApp
{
    public class CustomerDemo
    {
        public CustomerDemo()
        {
            Orders = new List<OrderDemo>();
        }

        public CustomerDemo(int customerId, int orderCount) 
        {
            this.CustomerId = customerId;
            this.OrderCount = orderCount;
   
        }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int OrderCount { get; set; }
        public List<OrderDemo> Orders { get; set; }
    }

    public class OrderDemo
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public List<ProductDemo> Products { get; set; }
    }

    public class ProductDemo
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }



    class Program
    {
        static void Main(string[] args)
        {
        //     using (var db = new NorthwindContext())
        //     {
        //         var city = "Miami";
        //         var customers = db.Customers.FromSqlRaw("select * from customers where city={0}",city).ToList();

        //         foreach (var item in customers)
        //         {
        //             Console.WriteLine(item.FirstName);
        //         }
        //     }
        // }


            using (var db = new CustomerNortwindContext())
            {
                var customers = db.CustomerOrders
                .FromSqlRaw("select c.id, c.first_name, count(*) as count from customers c inner join orders o on c.id=o.customer_id group by c.id").ToList();
                
                foreach (var item in customers)
                {
                    Console.WriteLine("Order id: {0} first name: {1} order count: {2}",item.CustomerId,item.FirstName,item.OrderCount);
                }
            }
        }

    }
}