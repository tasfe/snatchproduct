using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductApp
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void UpdateProduct(Product product);
    }

    public interface IPriceReducer
    {
        void ReducePrices(decimal priceReduction);
    }
}
