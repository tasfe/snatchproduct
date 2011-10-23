using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository reporsitory = null;
        public NavController(IProductRepository productRepository)
        {
            reporsitory = productRepository;
        }
        public PartialViewResult Menu(string category)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<String> categories = reporsitory.Products.Select(x => x.Category).Distinct().OrderBy(x => x);
            return PartialView(categories);
        }

    }
}
