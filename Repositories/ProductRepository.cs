using System.Collections.Generic;
using System.Linq;
using Balta.Data;
using Balta.Models;
using Balta.View.ProductView;
using Microsoft.EntityFrameworkCore;

namespace Balta.Repositories{

    public class ProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;            
        }

        public Product Find(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<ListProductView> Get()
        {
            return _context.Products
                    .Include(x => x.Category)
                    .Select(x => new ListProductView
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Price = x.Price,
                        Category = x.Category.Title,
                        CategoryId = x.CategoryId
                    })
                    .AsNoTracking()
                    .ToList();
        }

        public Product Get(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        public void Save(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}