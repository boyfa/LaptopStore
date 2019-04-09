using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopStore.Controllers
{
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        //can hien thi trang dang nhap


        public ActionResult Register()
        {
            return View();
        }
    }
}