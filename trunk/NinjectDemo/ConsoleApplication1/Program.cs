using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace NinjectDemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            ninjectKernel.Bind<IValueCalculator>().To<IterativeValueCalculator>().WhenInjectedInto<LimitShoppingCart>();
            //ninjectKernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize",50m);
            ninjectKernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("discountSize", 50m);

            // IValueCalculator valueCalculator = ninjectKernel.Get<IValueCalculator>();

            //ShoppingCart shopingCart = new ShoppingCart(valueCalculator);


           // ninjectKernel.Bind<ShoppingCart>().ToSelf().WithParameter("<parameterName>", <paramvalue>);

            //ninjectKernel.Bind<ShoppingCart>().To<LimitShoppingCart>().WithPropertyValue("ItemLimit", 20m);

            ShoppingCart shopingCart = ninjectKernel.Get<ShoppingCart>();
            Console.WriteLine("Total: {0:c}", shopingCart.CalculateStockValue());

            LimitShoppingCart limitShoppingCart = ninjectKernel.Get<LimitShoppingCart>() ;
            limitShoppingCart.ItemLimit = 1000000m;
            Console.WriteLine("Total2: {0:c}", limitShoppingCart.CalculateStockValue());

            Console.Read();
        }
    }
}
