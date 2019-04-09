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
    }
}