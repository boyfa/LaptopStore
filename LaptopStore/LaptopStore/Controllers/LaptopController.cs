﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopStore.Controllers
{
    public class LaptopController : Controller
    {
        // GET: Laptop
        public ActionResult Index()
        {
            return View();
        }
    }
}