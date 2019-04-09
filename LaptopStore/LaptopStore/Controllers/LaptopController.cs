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


        //===============================

        //chi tiet laptop
        // select id, ProductName, Price, Screen, Cpu, Ram, Vga, OS, Quantity
        // from LAPTOP l, LAPTOPDETAILS d
        // where l.id = d.id
        public ActionResult Details(int id)
        {


            var laptop = (from l1 in data.LAPTOPs
                          join l2 in data.LAPTOPDETAILs
                          on l1.ID equals l2.ID
                          where l1.ID == id
                          select new LaptopDetailsViewModel
                          {
                              id = l1.ID,
                              laptopName = l1.ProductName,
                              laptopImage = l1.ImageCover,
                              laptopPrice = (float)l1.Price,
                              laptopScreen = l2.Screen,
                              laptopCpu = l2.Cpu,
                              laptopVga = l2.Vga,
                              laptopOs = l2.OS,
                              laptopRam = l2.Ram,
                              laptopQuantity = (float)l2.Quantity
                          }).ToList();


            return View(laptop);

        }

    }
}