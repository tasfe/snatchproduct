using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjectDemo
{
    public class IterativeValueCalculator : IValueCalculator
    {
        public decimal ValueProducts(params Product[] products)
        {
            decimal totalValue = 0;
            foreach (Product p in products)
            {
                totalValue += p.Price;
            }
            return totalValue;
        }
    }
}
