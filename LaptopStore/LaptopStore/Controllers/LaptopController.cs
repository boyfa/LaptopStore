using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopStore.Controllers
{
    public class LaptopController : Controller
    {
        // GET: Laptop
        
        //////
        ///dbLaptopShopDataContext data = new dbLaptopShopDataContext();

        private List<LAPTOP> GetNewLaptop(int count)
        {
            return data.LAPTOPs.OrderByDescending(a => a.UpdateDate).Take(count).ToList();
        }
        // GET: Laptop
        public ActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var NewLaptop = GetNewLaptop(48);
            return View(NewLaptop.ToPagedList(pageNum, pageSize));
        }

        //laptop theo loai 
        public ActionResult MacLaptop(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var l = from laptop in data.LAPTOPs where laptop.IDM == 0 select laptop;
            return View(l.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Acer(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var l = from laptop in data.LAPTOPs where laptop.IDM == 2 select laptop;
            return View(l.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Asus(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var l = from laptop in data.LAPTOPs where laptop.IDM == 1 select laptop;
            return View(l.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Dell(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var l = from laptop in data.LAPTOPs where laptop.IDM == 3 select laptop;
            return View(l.ToPagedList(pageNum, pageSize));
        }

        public ActionResult HP(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var l = from laptop in data.LAPTOPs where laptop.IDM == 5 select laptop;
            return View(l.ToPagedList(pageNum, pageSize));

        }

        public ActionResult MSI(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var l = from laptop in data.LAPTOPs where laptop.IDM == 4 select laptop;
            return View(l.ToPagedList(pageNum, pageSize));

        }

    }
}