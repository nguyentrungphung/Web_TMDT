using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_1D2C.Models
{
    public class Giohang
    {
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();
        public int iMasp { set; get; }
        public string sTensp { set; get; }
        public string sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }

        }
        //Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
        public Giohang(int Masp)
        {
            iMasp = Masp;
            SANPHAM sp = data.SANPHAMs.Single(n => n.Masp == iMasp);
            sTensp = sp.Tensp;
            sAnhbia = sp.Anhbia;
            dDongia = double.Parse(sp.Giaban.ToString());
            iSoluong = 1;
        }
    }
}