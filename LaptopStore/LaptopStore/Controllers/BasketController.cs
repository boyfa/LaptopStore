using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopStore.Controllers
{
    public class BasketController : Controller
    {
        // GET: Basket
        public ActionResult Index()
        {
            return View();
        }
        // GET: Basket
        // GET: Basket

        dbLaptopShopDataContext data = new dbLaptopShopDataContext();
        public List<Basket> Laygiohang()
        {
            List<Basket> lstBasket = Session["Basket"] as List<Basket>;
            if (lstBasket == null)
            {

                lstBasket = new List<Basket>();
                Session["Basket"] = lstBasket;
            }
            return lstBasket;
        }

        public ActionResult Addbasket(int iMalaptop, string strURL)
        {
            List<Basket> lstBasket = Laygiohang();
            Basket sanpham = lstBasket.Find(n => n.iMalaptop == iMalaptop);
            if (sanpham == null)
            {
                sanpham = new Basket(iMalaptop);
                lstBasket.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        private int Tongsoluong()
        {
            int iTongsoluong = 0;
            List<Basket> lstBasket = Session["Basket"] as List<Basket>;
            if (lstBasket != null)
            {
                iTongsoluong = lstBasket.Sum(n => n.iSoluong);
            }
            return iTongsoluong;
        }

        private double Tongtien()
        {
            double iTongtien = 0;
            List<Basket> lstBasket = Session["Basket"] as List<Basket>;
            if (lstBasket != null)
            {
                iTongtien = lstBasket.Sum(n => n.dThanhtien);
            }
            return iTongtien;
        }

        public ActionResult Basket()
        {
            List<Basket> lstBasket = Laygiohang();
            if (lstBasket.Count == 0)
            {
                return RedirectToAction("Index", "Laptop");
            }
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstBasket);
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return PartialView();
        }

        public ActionResult Xoagiohang(int iMaSp)
        {
            List<Basket> lstBasket = Laygiohang();
            Basket sanpham = lstBasket.SingleOrDefault(n => n.iMalaptop == iMaSp);
            if (sanpham != null)
            {
                lstBasket.RemoveAll(n => n.iMalaptop == iMaSp);
                return RedirectToAction("Basket");
            }
            if (lstBasket.Count == 0)
            {
                return RedirectToAction("Index", "Laptop");
            }
            return RedirectToAction("Basket");
        }
    }
}