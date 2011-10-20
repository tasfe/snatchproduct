using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjectDemo
{
    public class ShoppingCart
    {
        protected IValueCalculator calculator;
        protected Product[] products = null;
        public ShoppingCart(IValueCalculator calcParam)
        {
            calculator = calcParam;

            products = new[] {
            new Product() { Name = "Kayak", Price = 275M},
            new Product() { Name = "Lifejacket", Price = 48.95M},
            new Product() { Name = "Soccer ball", Price = 19.50M},
            new Product() { Name = "Stadium", Price = 79500M}
            };
        }
        public virtual decimal CalculateStockValue()
        {
            // calculate the total value of the products
            decimal totalValue = calculator.ValueProducts(products);
            // return the result
            return totalValue;
        }
    }


    public class LimitShoppingCart : ShoppingCart
    {
        public LimitShoppingCart(IValueCalculator calcParam)
            : base(calcParam)
        {
            // nothing to do here
        }
        public override decimal CalculateStockValue()
        {
            // filter out any items that are over the limit
            var filteredProducts = products
            .Where(e => e.Price < ItemLimit);
            // perform the calculation
            return calculator.ValueProducts(filteredProducts.ToArray());
        }
        public decimal ItemLimit { get; set; }
    }
}
