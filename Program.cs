using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<User> Users { get; set; }      
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        // Yaptığımız link sorgusunun SQL karşılığı için
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });


        // Big Migration

        // Provider
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"server=localhost;port=3306;database=ShopDb;user=root;password=mysql123;";
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                // .UseSqlite("Data Source = shop.db");
                // .UseSqlServer(@"Data Source= .\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=SSPI; ");
                .UseMySql(connectionString,serverVersion);
        }
        // ProductCategory tablosunda iki tane primary key oluşturuyoruz.
        protected override void OnModelCreating( ModelBuilder modelBuilder){
             modelBuilder.Entity<ProductCategory>()
                            .HasKey(t => new {t.ProductId,t.CategoryId}); 
            modelBuilder.Entity<ProductCategory>()
                            .HasOne(pc => pc.Product)
                            .WithMany(p => p.ProductCategories)
                            .HasForeignKey(pc => pc.ProductId);
            modelBuilder.Entity<ProductCategory>()
                            .HasOne(pc => pc.Category)
                            .WithMany(c => c.ProductCategories)
                            .HasForeignKey(pc => pc.CategoryId);
        }

    }

    // One to Many
    // One to One
    // Many to many

    // Convention --> UserId yi karşılıklı olarak    public Customer Customer { get; set; }  public Supplier Supplier { get; set; }
    // data annotations  -->[Key] tanımlaması [Maxlength(200)] gibi tanımlamalar
    // fluent api (en baskın olan) en çok many to many de kullanılıyor


    public class User{
        // public User()
        // {
        //     this.Addresses = new List<Address>();
        // }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public List<Address> Addresses { get; set; }

    }
    public class Customer
    {
        public  int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User User { get; set; }  // Yabancı anahtar
        public int UserId { get; set; }  // One to One ilişki için 1 user 1 customerla denkleşmesi gerekiyor.
    }
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        
        public User User { get; set; }  // Yabancı anahtar
        public int UserId { get; set; }  // One to One ilişki için 1 user 1 customerla denkleşmesi gerekiyor.

    }

    public class Address
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }  // navigation property
        public int? UserId { get; set; } // int nullable değil varsayılan int => 0 Tablo boşken hata gönderektir. Bunun için ? kullanılıyor.
        
    }


    //[Entitiy] Product (Id,Name,Price)  => [DB] Product(Id,Name,Price)
    public class Product
    {
        // Primary Key(Id, <type_name> Id)
        // [Key]
        public int Id { get; set; }
        public string Name { get; set; }  //string varsayılan olarak nullable
        public decimal? Price { get; set; } //decimal varsayılan olarak nonnullable ancak ?--> nullable değer yapar.
        public List<ProductCategory> ProductCategories { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }

    public class ProductCategory
    {
        public int ProductId { get; set; }
        public  Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShopContext())
            {
                var products = new List<Product>()
                {
                    new Product(){Name="Samsung S5",Price=2000},
                    new Product(){Name="Samsung S6", Price=3000},              
                    new Product(){Name="Samsung S7", Price=4000},
                    new Product(){Name="Samsung S8", Price=5000},
                    new Product(){Name="Samsung S9", Price=6000},
                };
                // db.Products.AddRange(products);

                var categories = new List<Category>()
                {
                    new Category(){Name="Elektronik"},
                    new Category(){Name="Telefon"},
                    new Category(){Name="Bilgisayar"},
                    
                };
                // db.Categories.AddRange(categories);

                int[] ids = new int[2]{1,2};

                var p = db.Products.Find(1);

                p.ProductCategories = ids.Select(cid=> new ProductCategory(){
                    CategoryId = cid,
                    ProductId= p.Id
                }).ToList();
                db.SaveChanges();
                
            }
            
        }

        static void InsertUsers()
        {
            var users = new List<User>(){
                new User(){Username="sadikTuran", Email="info@sadikturan.com"},
                new User(){Username="myegnidemir", Email="info@myegnidemir.com"},
                new User(){Username="akifDere", Email="info@akifDere.com"},
                new User(){Username="aculcu", Email="info@aculcu.com"},
                new User(){Username="bculcu", Email="info@bculcu.com"},                
                new User(){Username="nculcu", Email="info@nculcu.com"},
            };

            using (var db = new ShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }

        static void InsertAddresses()
        {
            var addresses = new List<Address>(){
                new Address(){FullName="sadikTuran", Title="Ev adresi", Body="Kocaeli", UserId=1},
                new Address(){FullName="sadikTuran", Title="İş adresi", Body="Kocaeli",UserId=1},
                new Address(){FullName="myegnidemir", Title="İş adresi", Body="İstanbul",UserId=2},            
                new Address(){FullName="myegnidemir", Title="Ev adresi",Body="İstanbul",UserId=2},
                new Address(){FullName="akifDere", Title="Ev adresi",Body="Van",UserId=3},
                new Address(){FullName="aculcu", Title="İş adresi",Body="Gaziantep",UserId=4},
                new Address(){FullName="bculcu", Title="Ev adresi",Body="Besni",UserId=5},           
                new Address(){FullName="nculcu", Title="İş adresi",Body="Besni",UserId=6},

            };

            using (var db = new ShopContext())
            {
                db.Addresses.AddRange(addresses);
                db.SaveChanges();
            }
        }



    }
}