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
            using (var db = new NorthwindContext())
            {
                // Sipariş sayısı 0 dan büyük olan customer ların FirstName i getirme
                var customers = db.Customers
                                    .Where(i => i.Orders.Any())
                                    //.Where(i => i.Orders.Count()>0)
                                    .Select(i => new CustomerDemo 
                                    { 
                                        CustomerId = i.Id,
                                        Name = i.FirstName,
                                        OrderCount = i.Orders.Count(),
                                        Orders = i.Orders.Select( a => new OrderDemo{
                                            OrderId = a.Id,
                                            Total = (decimal)a.OrderDetails.Sum(od => od.Quantity*od.UnitPrice),
                                            Products = a.OrderDetails.Select(p => new ProductDemo
                                            {
                                                ProductId = (int)p.ProductId ,
                                                Name = p.Product.ProductName
                                            }).ToList() 
                                        }).ToList()
                                        
                                    })
                                    .OrderBy(i => i.CustomerId)
                                    .ToList();
                foreach (var customer in customers)
                {
                    Console.WriteLine($"id: {customer.CustomerId} name: {customer.Name} count:{customer.OrderCount}");
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine($"Order id: {order.OrderId} total : {order.Total}");
                        foreach (var product in order.Products)
                        {
                            Console.WriteLine($"Product Id : {product.ProductId} name: {product.Name}");
                        }
                    }
                }

            }
 
        }


    }
}