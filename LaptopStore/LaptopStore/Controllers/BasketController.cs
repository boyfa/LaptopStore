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

        public ActionResult Capnhatgiohang(int iMaSP, FormCollection f)
        {
            List<Basket> lstBasket = Laygiohang();
            Basket sanpham = lstBasket.SingleOrDefault(n => n.iMalaptop == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Basket");
        }

        public ActionResult Xoatatcagiohang()
        {
            List<Basket> lstBasket = Laygiohang();
            lstBasket.Clear();
            return RedirectToAction("Index", "Laptop");
        }

        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Basket"] == null)
            {
                return RedirectToAction("Index", "Laptop");
            }
            List<Basket> lstBasket = Laygiohang();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstBasket);

        }

        public ActionResult Dathang(FormCollection collection)
        {
            //initialize new object order
            order ddh = new order();
            //........... object customer
            customer cu = (customer)Session["Taikhoan"];
            //get basket 
            //what is basket. 

            List<Basket> gh = Laygiohang();
            ddh.IDC = cu.IDC;
            ddh.OrderDate = DateTime.Now;
            var ngaygiao = String.Format("{0:dd/MM/yyyy}", collection["Ngaygiao"]);
            var reciever = collection["reciever"];
            var dc = collection["Place"];
            var sdtnguoinhan = collection["sdt"];
            ddh.DiliverDate = DateTime.Parse(ngaygiao);
            ddh.Reciever = reciever;
            ddh.Place = dc;
            ddh.Phone = sdtnguoinhan;
            ddh.Deliver = false;
            ddh.Payment = false;
            data.orders.InsertOnSubmit(ddh);
            data.SubmitChanges();

            foreach (var item in gh)
            {
                ordersdetail orde = new ordersdetail();
                // details of the order: orde
                orde.IDO = ddh.IDO;

                orde.ID = item.iMalaptop;
                orde.Number = item.iSoluong;
                orde.Price = item.dDongia;
                data.ordersdetails.InsertOnSubmit(orde);
                data.SubmitChanges();
            }

            data.SubmitChanges();

            Session["Basket"] = null;
            return RedirectToAction("Xacnhandonhang", "Basket");
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }

    }
}