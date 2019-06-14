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
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-HIEP91V; Database=prodcat;User ID=sa;password=root");    
            //optionsBuilder.UseSqlServer(@"Server=tcp:basicprodcat.database.windows.net,1433;Initial Catalog=prodcat;Persist Security Info=False;User ID=danilo.;Password=H@loprimordi1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");        
        } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}