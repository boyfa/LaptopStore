using LaptopStore.Models;
using LaptopStore.ViewModel;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace LaptopStore.Controllers
{
    public class AdminController : Controller
    {
        dbLaptopShopDataContext data = new dbLaptopShopDataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Laptop(int? page)
        {
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("Dangnhap", "NguoiDung");
            }
            int pageNumber = (page ?? 1);
            int pageSiZe = 7;
            return View(data.LAPTOPs.ToList().OrderBy(n => n.ID).ToPagedList(pageNumber, pageSiZe));
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["username"];
            var matkhau1 = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau1))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                administrator ad = data.administrators.SingleOrDefault(n => n.taikhoan == tendn && n.matkhau == matkhau1);
                if (ad != null)
                {
                    //ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.IDM = new SelectList(data.manufacturers.ToList().OrderBy(n => n.ManufacturerName), "IDM", "ManufacturerName");

            return View();
        }



        [HttpPost]
        [ValidateInput(false)]
        // sửa fileupload thành fileUpload
        public ActionResult Create(LAPTOP laptop, HttpPostedFileBase fileUpload)
        {
            //Đưa dữ liệu vào dropdownlist


            ViewBag.IDM = new SelectList(data.LAPTOPs.ToList().OrderBy(n => n.ProductName), "IDM", "ProductName");
            //Kiểm tra đường dẫn file

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            //Thêm vào CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Lưu tên file, lưu ý bổ sung thư viện System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    //Kiểm tra hình ảnh tồn tại chưa
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        //Lưu hình ảnh vào đường dẫn
                        fileUpload.SaveAs(path);
                    }
                    laptop.ImageCover = fileName;
                    //lưu vào CSDL 
                    data.LAPTOPs.InsertOnSubmit(laptop);
                    data.SubmitChanges();
                }
                return RedirectToAction("Laptop");
            }
        }

        //HIển thị sản phẩm
        public ActionResult Chitiet(int id)
        {
            //Lấy ra đối tượng sách theo mã 
            LAPTOP laptop = data.LAPTOPs.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = laptop.ID;
            if (laptop == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(laptop);
        }
        [HttpGet]
        public ActionResult XoaSP(int id)
        {
            LAPTOP laptop = data.LAPTOPs.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = laptop.ID;
            if (laptop == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(laptop);
        }
        [HttpPost, ActionName("XoaSP")]
        public ActionResult Xacnhanxoa(int id)
        {
            //Lay61 ra đối tượng sách cần xóa theo mã
            LAPTOP laptop = data.LAPTOPs.SingleOrDefault(n => n.ID == id);
            ViewBag.ID = laptop.ID;
            if (laptop == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.LAPTOPs.DeleteOnSubmit(laptop);
            data.SubmitChanges();
            return RedirectToAction("Laptop");
        }
        [HttpGet]
        public ActionResult SuaSP(int id)
        {
            //Lấy ra đối tượng sách theo mã
            LAPTOP laptop = data.LAPTOPs.SingleOrDefault(n => n.ID == id);
            if (laptop == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            //Đưa dữ liệu vào dropdownlist
            //Lấy ds từ tab kế chủ đề,sắp xếp tăng dần theo tên chủ đề, chọn lấy giá trị MaCD, hiển thị TenChude
            ViewBag.IDM = new SelectList(data.manufacturers.ToList().OrderBy(n => n.ManufacturerName), "IDM", "ManufacturerName", laptop.IDM);
            return View(laptop);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSP(LAPTOP laptop, HttpPostedFileBase fileUpload)
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.IDM = new SelectList(data.manufacturers.ToList().OrderBy(n => n.ManufacturerName), "IDM", "ManufacturerName");
            //Kiểm tra đường dẫn file

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh ";
                return View();
            }
            //Thêm vào CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Lưu tên file, lưu ý bổ sung thư viện System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    //Kiểm tra hình ảnh tồn tại chưa
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        //Lưu hình ảnh vào đường dẫn
                        fileUpload.SaveAs(path);
                    }
                    laptop.ImageCover = fileName;
                    //lưu vào CSDL 
                    UpdateModel(laptop);
                    data.SubmitChanges();
                }
                return RedirectToAction("Laptop");

            }

        }

        public ActionResult Search(string search)
        {
            var query = from l in data.LAPTOPs
                        select l;

            if (!String.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.ProductName.Contains(search));
            }

            var dulieu = query.ToList();

            return View(dulieu);
        }

        

    }
}
