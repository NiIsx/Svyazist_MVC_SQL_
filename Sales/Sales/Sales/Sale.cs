using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Sales
{
    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {

        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; }
    }

    public class SalesStatistic
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; }
        public int CumulativeSales { get; set; }
        public decimal CumulativeCost { get; set; }
    }
}
