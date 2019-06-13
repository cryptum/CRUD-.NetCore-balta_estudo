using Microsoft.EntityFrameworkCore;
using Balta.Models;
using Balta.Data.Mapping;

namespace Balta.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set;}
        public DbSet<Category> Categories { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=basicprodcat;
                                            Database=prodcat;
                                            User ID=sa;
                                            password=H@loprimordi1" 
                                            );
        } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}