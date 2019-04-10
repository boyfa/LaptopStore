using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaptopStore.ViewModel
{
    public class LaptopDetailsViewModel
    {
        public int id { get; set; }
        public string laptopName { get; set; }
        public double laptopPrice { get; set; }
        public string laptopImage { get; set; }
        public string laptopScreen { get; set; }
        public string laptopCpu { get; set; }
        public string laptopRam { get; set; }
        public string laptopVga { get; set; }
        public string laptopOs { get; set; }
        public float laptopQuantity { get; set; }
    }
}