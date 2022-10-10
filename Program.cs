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

    }

    // One to Many
    // One to One
    // Many to many

    public class User{
        // public User()
        // {
        //     this.Addresses = new List<Address>();
        // }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public List<Address> Addresses { get; set; }

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
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }  //string varsayılan olarak nullable
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Price { get; set; } //decimal varsayılan olarak nonnullable ancak ?--> nullable değer yapar.
        public int CategoryId { get; set; }

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
            // InsertUsers();
            // InsertAddresses();

            using (var db = new ShopContext())
            {
                var user = db.Users.FirstOrDefault(i=>i.Username=="myegnidemir");
                if (user!=null)
                {
                    user.Addresses = new List<Address>();

                    user.Addresses.AddRange
                    (
                        new List<Address>()
                        {
                            new Address(){FullName="myegnidemir", Title="İş adresi 1", Body="İstanbul"},
                            new Address(){FullName="myegnidemir", Title="İş adresi 2", Body="İstanbul"},
                            new Address(){FullName="myegnidemir", Title="İş adresi 3", Body="İstanbul"},
                        }
                    );
                    
                    db.SaveChanges();
                }
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