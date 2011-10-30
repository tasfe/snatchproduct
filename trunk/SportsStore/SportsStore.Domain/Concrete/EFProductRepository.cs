using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Data;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {

            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                context.Entry(product).State = EntityState.Modified;
            }

            //Product prop = Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            //if (prop != null)
            //{
            //    prop.Category = product.Category;
            //    prop.Description = product.Description;
            //    prop.ImageData = product.ImageData;
            //    prop.ImageMimeType = product.ImageMimeType;
            //    prop.Name = product.Name;
            //    prop.Price = product.Price;
            //}


            //System.Data.EntityState statebefore = context.Entry(product).State;
            //context.Entry(product).State = System.Data.EntityState.Modified;

          //  Product orangeProduct = Products.FirstOrDefault(p => p.ProductId == product.ProductId);
          
           
            context.SaveChanges();


        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

    }
}
