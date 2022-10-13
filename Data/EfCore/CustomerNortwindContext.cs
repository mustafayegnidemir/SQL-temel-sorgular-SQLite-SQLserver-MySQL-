using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace consoleApp.Data.EfCore
{
    public class CustomerOrder
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public int OrderCount { get; set; }
    }
    public class CustomerNortwindContext: NorthwindContext
    {
        public DbSet<CustomerOrder> CustomerOrders { get; set;}

        public CustomerNortwindContext()
        {
            
        }
        public CustomerNortwindContext(DbContextOptions<NorthwindContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<CustomerOrder>(entity => 
             {
                entity.HasNoKey();
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.CustomerId).HasColumnName("id");
                entity.Property(e => e.OrderCount).HasColumnName("count");
             });
        }
    }
}