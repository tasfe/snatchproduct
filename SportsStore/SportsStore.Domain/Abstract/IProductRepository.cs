﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        void DeleteProduct(Product product);
    }
}
