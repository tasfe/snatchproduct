using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductApp
{
    public class MyPriceReducer : IPriceReducer
    {
        private IProductRepository repository;
        public MyPriceReducer(IProductRepository repo)
        {
            repository = repo;
        }

        public void ReducePrices(decimal priceReduction)
        {
            foreach (Product p in repository.GetProducts())
            {
                p.Price = Math.Max(p.Price - priceReduction, 1);
                repository.UpdateProduct(p);
            }
        }
    }
}
