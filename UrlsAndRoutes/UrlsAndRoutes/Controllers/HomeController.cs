﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ViewResult CustomVariable(string action,string id)
        {
            ViewBag.CustomVariable = id;
            ViewBag.ActionName = action;
            return View();
        }
    }
}
