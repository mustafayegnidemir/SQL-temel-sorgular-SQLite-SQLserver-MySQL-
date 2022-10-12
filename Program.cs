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

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {
            // Tüm müşteri kayıtlarını getiriniz. (ToList())
                // var customers = db.Customers.ToList();

                // foreach (var item in customers)
                // {
                //     Console.WriteLine(item.FirstName +" "+ item.LastName);
                // }

            // Tüm müşteri kayıtlarının sadece first_name ve last_name bilgilerini getiriniz. (select(c => new {} c.FirstName))  anonymous type
                // var customers = db.Customers.Select(c => new {
                //     c.FirstName,
                //     c.LastName
                // });

                // foreach (var item in customers)
                // {
                //     Console.WriteLine(item.FirstName +" "+ item.LastName);
                // }



            // New York' da yaşayan müşterileri isim sırasına göre getiriniz. .Where(i => i.City =="New York")
                // var customers = db.Customers
                //                     .Where(i => i.City =="New York")
                //                     .Select(s => new {s.FirstName,s.LastName})
                //                     .ToList();

                // foreach (var item in customers)
                // {
                //     Console.WriteLine(item.FirstName +" "+ item.LastName);
                // }
                // "Beverages" kategorisine ait ürünlerin isimlerini getiriniz.

                // var productnames = db.Products
                //                     .Where(i => i.Category =="Beverages")
                //                     .Select(s => new {s.ProductName})
                //                     .ToList();

                // foreach (var item in productnames)
                // {
                //     Console.WriteLine(item.ProductName);
                // }




            // En son eklenen 5 ürün bilgisini alınız.(.Take(5) --> tablonun en üstünden 5 tane değeri getirir.)(OrderByDescending(i => i.Id))

                // var products = db.Products
                //                     .OrderByDescending(i => i.Id)
                //                     .Select(s => new {s.ProductName})
                //                     .Take(5);
                // foreach (var item in products)
                // {
                //     Console.WriteLine(item.ProductName);
                // }


            // Fiyatı 10 ile 30 arasında olan ürünlerin isim, fiyat bilgilerini azalan şekilde getiriniz.
                // var products = db.Products
                //                     .Where(i => i.ListPrice>=10 && i.ListPrice<=30)
                //                     .Select(s => new {s.ProductName, s.ListPrice})
                //                     .OrderByDescending(i => i.ListPrice)
                //                     .ToList();
                // foreach (var item in products)
                // {
                //     Console.WriteLine(item.ProductName+" "+item.ListPrice);
                // }



            // "Beverages" kategorisindeki ürünlerin ortalama fiyatı nedir?
                // var ortalama = db.Products
                //                     .Where(i => i.Category == "Beverages")
                //                     .Average(i => i.ListPrice);

                // Console.WriteLine("Ortalama Liste Fiyatı : {0}", ortalama);




            // "Beverages" kategorisinde kaç ürün vardır?
                // var categoryCount = db.Products
                //                          .Where(i => i.Category == "Beverages")
                //                          .Count();
                // Console.WriteLine("Beverages kategorisinde ki ürün sayısı {0} ", categoryCount);
                // //2. Yöntem
                // var adet = db.Products.Count(i => i.Category =="Beverages");
                // Console.WriteLine("Beverages kategorisinde ki ürün sayısı {0} ", adet);

            // "Beverages" veya "Condiments" kategorilerindeki ürünlerin toplam fiyatı nedir?
                // var toplam = db.Products
                //                     .Where(i => i.Category == "Beverages" || i.Category =="Condiments")
                //                     .Sum(i => i.ListPrice);

                // Console.WriteLine("Toplam Liste Fiyatı : {0}", toplam);

            // 'Tea' kelimesini içeren ürünleri getiriniz.
                // var urunlerTea = db.Products
                //                     .Where(i => i.ProductName.ToLower().Contains("Tea".ToLower()) || i.Description.ToLower().Contains("Tea".ToLower()))
                //                     .ToList();
                // foreach (var item in urunlerTea)
                // {
                //     Console.WriteLine(item.ProductName);
                // }


            // En pahalı ürün ve en ucuz ürün hangisidir?
                // var pahali = db.Products.Max(i => i.ListPrice);
                // var ucuz = db.Products.Min(i => i.ListPrice);

                // Console.WriteLine("En ucuz ürünün fiyatı {0} iken en pahalı ürün {1} dir.",ucuz,pahali);

                // var productPahali = db.Products.Where(i => i.ListPrice ==pahali).FirstOrDefault();
                // var productUcuz = db.Products.Where(i => i.ListPrice ==ucuz).FirstOrDefault();

                // // var productPahali = db.Products.Where(i => i.ListPrice ==db.Products.Max(a => a.ListPrice)).FirstOrDefault();
                // // var productUcuz = db.Products.Where(i => i.ListPrice ==db.Products.Max(b => b.ListPrice)).FirstOrDefault();

                // Console.WriteLine($"En ucuz ürün adı {productUcuz.ProductName} iken en pahalı ürün ise {productPahali.ProductName} dir.");

            }
 
        }


    }
}