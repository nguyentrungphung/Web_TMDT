using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_1D2C.Models;
using PagedList;
using PagedList.Mvc;

namespace Web_1D2C.Controllers
{
    public class ShoesController : Controller
    {
        // GET: Shoes
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();

        

        private List<SANPHAM> Layspmoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var spmoi = Layspmoi(8);
            return View(spmoi.ToPagedList(pageNum,pageSize));
        }
        public ActionResult Nhasanxuat()
        {
            var nhasanxuat = from nsx in data.NHASAXUATs select nsx;
            return PartialView(nhasanxuat);
        }
       
        public ActionResult Loai(int ID)
        {
            var loai = from l in data.LOAIs where l.MaNSX==ID select l;
            return PartialView(loai);
        }
        public ActionResult SPtheoLoai(int id)
        {
            var sanpham = from sp in data.SANPHAMs where sp.MaLoai == id select sp;
            return View(sanpham);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var sanpham = from sp in data.SANPHAMs where sp.Masp == id select sp;
            return View(sanpham.Single());
        }
        [HttpGet]
        public ActionResult SPlienquan(int id)
        {
            var sanpham = from sp in data.SANPHAMs where sp.MaLoai == id select sp;
            return PartialView(sanpham);
        }
    }
}