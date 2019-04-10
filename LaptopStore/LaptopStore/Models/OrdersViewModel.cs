using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaptopStore.ViewModel
{
    public class OrdersViewModel
    {
        public int ido { get; set; }
        public string TenkhachHang { get; set; }
        public string SdtKhachHang { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public string TenNguoiNhan { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string TenSanPham { get; set; }

        public int SoLuong { get; set; }
        public double Gia { get; set; }



    }
}