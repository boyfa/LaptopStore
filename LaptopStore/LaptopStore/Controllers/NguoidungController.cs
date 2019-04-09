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
        // GET: Nguoidung
        dbLaptopShopDataContext data = new dbLaptopShopDataContext();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Dangky(FormCollection collection, customer cu)
        {
            var hoten = collection["CustomersName"];
            var tendn = collection["Account"];
            var matkhau = collection["passwords"];
            var nhaplaimatkhau = collection["ConfirmPassword"];
            var Diachi = collection["Addresses"];
            var email = collection["Email"];
            var dienthoai = collection["PhoneNumber"];

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng ko được để trống";

            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";

            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi4"] = "Email không được bỏ trống";

            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại";

            }
            else if (String.IsNullOrEmpty(nhaplaimatkhau) && nhaplaimatkhau != matkhau)
            {
                ViewData["Loi7"] = "Phải xác nhận lại mật khẩu";

            }
            else
            {
                cu.CustomersName = hoten;
                cu.Account = tendn;
                cu.Passwords = matkhau;
                cu.Email = email;
                cu.Addresses = Diachi;
                cu.PhoneNumber = dienthoai;
                data.customers.InsertOnSubmit(cu);
                data.SubmitChanges();
                Session["Test"] = cu;
                return RedirectToAction("Dangnhap");


            }
            return this.Dangky();

        }
    }
}