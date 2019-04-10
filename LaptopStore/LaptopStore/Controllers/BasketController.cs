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
    }
}