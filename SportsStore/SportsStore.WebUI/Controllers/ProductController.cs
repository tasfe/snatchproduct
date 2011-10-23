﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 3;
        private IProductRepository repository;

        public ProductController(IProductRepository productsRepository)
        {
            repository = productsRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel viewModel = new ProductsListViewModel()
            {
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(p => p.Category == category).Count() },
                CurrentCategory = category
            };

            return View(viewModel);
        }
    }
}
