using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace consoleApp
{
    // Entity Class 
    public class ShopContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // Yaptığımız link sorgusunun SQL karşılığı için
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });


        // Big Migration

        // Provider
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlite("Data Source = shop.db");
        }

    }


    //[App] Product (Id,Name,Price)  => [DB] Product(Id,Name,Price)
    public class Product
    {
        // Primary Key(Id, <type_name> Id)
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }  //string varsayılan olarak nullable
        public decimal? Price { get; set; } //decimal varsayılan olarak nonnullable ancak ?--> nullable değer yapar.

    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {

            // AddProduct();
            // GetAllProducts();
            // GetAllProductById(1);
            // GetAllProductByName("samsung");
            // UpdateProduct();
            DeleteProduct(13);
        }

        static void DeleteProduct(int id){
            using(var db = new ShopContext())
            {
                var p = new Product(){Id = id};
                // db.Products.Remove(p); // veya 
                db.Entry(p).State = EntityState.Deleted;
                db.SaveChanges();
                Console.WriteLine("Veri silindi.");


            }
            // using(var db = new ShopContext())
            // {
            //     var p = db.Products.FirstOrDefault(i => i.Id==id);

            //     if (p!=null)
            //     {
            //         //db.Remove(p); // veya
            //         db.Products.Remove(p);
            //         db.SaveChanges();
            //         Console.WriteLine("Veri silindi.");

            //     }

            // }
        }

        static void UpdateProduct()
        {
            // using(var db = new ShopContext())
            // {
            //     var products = new List<Product>();
            //     // Name e göre Update işlemi primarykey değil.
            //     products = db.Products.Where(i => i.Name == "Samsung S5").ToList();
            //     foreach (var p in products)
            //     {
            //         if (p!=null)
            //         {
            //             p.Price = 2400;
            //             db.Products.Update(p);

            //             db.SaveChanges();
            //         }
            //     }
            // }  

            // using(var db = new ShopContext())
            // {

                // // Id ye göre Update işlemi
                // var p = db.Products.Where(i => i.Id ==1).FirstOrDefault();
                // if (p!=null)
                // {
                //     p.Price = 2400;
                //     db.Products.Update(p);

                //     db.SaveChanges();
                // }
            // }        

            // using(var db = new ShopContext())
            // {
            //     // Attach metodunu kullanırken primary key ile belirlenen entity üzerinde update işlemi(EntityState.Unchanged) yapıyoruz. Eğer Primarykey üzerinden entity belirlenmez ise örneğin {Name=" Samsung S5"} database e yeni entity eklenmiş olunur. Yani EntityState.Added --> INSERT INTO komutu çalışır.
            //     var entity = new Product(){Id = 1};
            //     // var entity = new Product(){Name ="Samsung S5"};
            //     db.Products.Attach(entity);
            //     entity.Price = 3000;
            //     db.SaveChanges();

            // }
            
            // using(var db = new ShopContext())
            // {

            //     //change tracking
            //     var p = db.Products
            //             // .AsNoTracking()
            //             .Where(i => i.Id ==2)
            //             .FirstOrDefault();
            //     if (p!=null)
            //     {
            //         //%20 zam yapıldı.
            //         p.Price *= 1.2m;
            //         db.SaveChanges();

            //         Console.WriteLine("Güncelleme yapıldı.");
            //     }
            // }
        }

        static void AddProducts()
        {

            using (var db = new ShopContext())
            {
                var products = new List<Product>{
                        new Product{Name ="Samsung S5", Price =2000},
                        new Product{Name ="Samsung S6", Price =3000},
                        new Product{Name ="Samsung S7", Price =4000},
                        new Product{Name ="Samsung S8", Price =5000},
                        new Product{Name ="Samsung S9", Price =6000},
                    };
                // foreach (var item in products)
                // {
                //     db.Products.Add(item);
                // }
                db.Products.AddRange(products);
                db.SaveChanges();
                Console.WriteLine("Veriler Eklendi.");
            }

        }
        static void AddProduct()
        {

            using (var db = new ShopContext())
            {
                var p = new Product { Name = "Samsung S5", Price = 2000 };

                db.Products.Add(p);
                db.SaveChanges();
                Console.WriteLine("Veriler Eklendi.");
            }

        }
        
         static void GetAllProductById(int id)
        {
            using (var context = new ShopContext())
            {
                var result = context
                            .Products
                            .Where(p => p.Id ==id)
                            .Select(p => new {p.Name,p.Price})
                            .FirstOrDefault ();


                Console.WriteLine($"name: {result.Name} price: {result.Price} ");
            }
        }


        static void GetAllProductByName(string name)
        {
            using (var context = new ShopContext())
            {
                var products = context
                            .Products
                            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                            //  .Where(p => p.Price>2000 && p.Name =="samsung")
                            .Select(p => new {p.Name,p.Price})
                            .ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"name: {p.Name} price: {p.Price} ");
                }
            }
        }

        static void GetAllProducts()
        {
            using (var context = new ShopContext())
            {
                var products = context
                .Products
                .Select(p => new {p.Name,p.Price})
                .ToList();

                foreach (var p in products)
                {
                    Console.WriteLine($"name: {p.Name} price: {p.Price} ");
                }

            }
        }


    }
}