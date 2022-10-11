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
            // tablo ismini db tarafında değiştirme -->Taabloyu map etme 
            modelBuilder.Entity<Product>()
                        .ToTable("ürünler");
            modelBuilder.Entity<User>()
                        .HasIndex(u=>u.Username)
                        .IsUnique();
            modelBuilder.Entity<Customer>()
                        .Property(p => p.IdentityNumber)
                        .HasMaxLength(15)
                        .IsRequired();
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

    // 1-Convention --> UserId yi karşılıklı olarak    public Customer Customer { get; set; }  public Supplier Supplier { get; set; }
    // 2-data annotations  -->[Key] tanımlaması [Maxlength(200)] gibi tanımlamalar
    // 3-fluent api (en baskın olan) en çok many to many de kullanılıyor


    public class User{
        // public User()
        // {
        //     this.Addresses = new List<Address>();
        // }
        public int Id { get; set; }
        [Required]
        [MaxLength(15), MinLength(8)]
        public string Username { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string Email { get; set; }
        public Customer Customer { get; set; }
        public Supplier Supplier { get; set; }
        public List<Address> Addresses { get; set; }

    }
    public class Customer
    {
        [Column("customer_id")]
        public  int Id { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [NotMapped]
        public string FullName { get; set; }
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }  //string varsayılan olarak nullable
        public decimal? Price { get; set; } //decimal varsayılan olarak nonnullable ancak ?--> nullable değer yapar.
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime InsertedDate { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public List<ProductCategory> ProductCategories { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }

    // [NotMapped] // Product category database de tablo olarak görülmeyecek
    [Table("UrunKategorileri")] // database tarafında görülecek tablo adı
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
                // var p = new Product()
                // {
                //     Name = "Samsung S6",
                //     Price = 2000
                // };
                // db.Products.Add(p);

                var p = db.Products.FirstOrDefault(); 
                p.Name = "Samsung S10";
                db.SaveChanges();

                
            }
            
        }


    }
}