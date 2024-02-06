using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebAPICRID.Models
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options) : base (options)
        {

        }

        public DbSet<Brand> Brands { get;set; }
        public DbSet<Product> Products { get; set; }

    }
}   
