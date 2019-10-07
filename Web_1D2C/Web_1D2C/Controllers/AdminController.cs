using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_1D2C.Models;

namespace Web_1D2C.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sanpham()
        {
            return View(data.SANPHAMs.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var tendn = f["username"];
            var matkhau = f["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phai nhap ten dang nhap";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phai nhap mat khau";
            }
            else
            {
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tai khoan khong ton tai";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Themmoisp()
        {
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(data.NHASAXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisp( SANPHAM sp, HttpPostedFileBase fup)
        {
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(data.NHASAXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if(fup== null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fup.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại ";
                    }
                    else
                    {
                        fup.SaveAs(path);
                    }
                    sp.Anhbia = fileName;
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sanpham");
            }  
        }

        public ActionResult Chitiet(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            ViewBag.Masp = sp.Masp;
            if(sp== null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        public ActionResult Xoasp(int id)
        {
            
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            ViewBag.Masach = sp.Masp;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost,ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            ViewBag.Masp = sp.Masp;
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SANPHAMs.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("Sanpham");
        }
        [HttpGet]
        public ActionResult Suasp(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            if(sp== null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(data.NHASAXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasp (SANPHAM sp, HttpPostedFileBase fup)
        {
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(data.NHASAXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if(fup==null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fup.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), filename);
                    if(System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fup.SaveAs(path);
                    }
                    sp.Anhbia = filename;
                    UpdateModel(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sanpham");
            }
        }

        public ActionResult NSX()
        {
            return View(data.NHASAXUATs.ToList());
        }
        [HttpGet]
        public ActionResult ThemmoiNSX()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiNSX(NHASAXUAT nsx)
        {
            data.NHASAXUATs.InsertOnSubmit(nsx);
            data.SubmitChanges();
            return RedirectToAction("NSX");
        }
        public ActionResult Chitietnsx(int id)
        {
            NHASAXUAT nsx = data.NHASAXUATs.SingleOrDefault(n => n.MaNSX == id);
            ViewBag.MaNSX = nsx.MaNSX;
            if(nsx==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nsx);
        }

        [HttpGet]
        public ActionResult Xoansx(int id)
        {
            NHASAXUAT nsx = data.NHASAXUATs.SingleOrDefault(n => n.MaNSX == id);
            ViewBag.MaNSX = nsx.MaNSX;
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nsx);
        }

        [HttpPost,ActionName("Xoansx")]
        public ActionResult xacnhanxoansx(int id)
        {
            NHASAXUAT nsx = data.NHASAXUATs.SingleOrDefault(n => n.MaNSX == id);
            ViewBag.MaNSX = nsx.MaNSX;
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.NHASAXUATs.InsertOnSubmit(nsx);
            data.SubmitChanges();
            return View(nsx);
        }
        [HttpGet]
        public ActionResult Suansx(int id)
        {
            NHASAXUAT nsx = data.NHASAXUATs.SingleOrDefault(n => n.MaNSX == id);
        
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nsx);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suansx(NHASAXUAT nsx)
        {
            UpdateModel(nsx);
            data.SubmitChanges();
            return RedirectToAction("NSX");
        }

        public ActionResult Loai()
        {
            return View(data.LOAIs.ToList());
        }
        [HttpGet]
        public ActionResult Themmoiloai()
        {
            ViewBag.MaNSX = new SelectList(data.NHASAXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();

        }

        [HttpPost]
        public ActionResult Themmoiloai(LOAI l)
        {
            data.LOAIs.InsertOnSubmit(l);
            data.SubmitChanges();
            return RedirectToAction("Loai");
        }
        public ActionResult Chitietloai(int id)
        {
            LOAI l = data.LOAIs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = l.MaLoai;
            if(l== null)
            {
                Response.StatusCode=404;
                return null;
            }
            return View(l);
        }
        [HttpGet]
        public ActionResult Xoaloai(int id)
        {
            LOAI l = data.LOAIs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaNSX = l.MaNSX;
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(l);
        }

        [HttpPost, ActionName("Xoaloai")]
        public ActionResult xacnhanxoaloai(int id)
        {
            LOAI l = data.LOAIs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaNSX = l.MaNSX;
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.LOAIs.InsertOnSubmit(l);
            data.SubmitChanges();
            return View(l);
        }
        [HttpGet]
        public ActionResult Sualoai(int id)
        {
            LOAI l = data.LOAIs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaNSX = new SelectList(data.NHASAXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(l);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sualoai(LOAI l)
        {
            ViewBag.MaNSX = new SelectList(data.NHASAXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            UpdateModel(l);
            data.SubmitChanges();
            return RedirectToAction("Loai");
        }
  
        public ActionResult Dondathang()
        {
            return View(data.DONDATHANGs.ToList());
        }
        public ActionResult CT_Dondathang(int id, bool? giaohang)
        {
            ViewBag.ID = id;
            //return View(data.CHITIETDONTHANGs.ToList());
            var _donhang = from ddh in data.DONDATHANGs where ddh.MaDonHang == id select ddh;
            var donhang = _donhang.FirstOrDefault();
            ViewBag.GiaoHang = donhang.Tinhtranggiaohang;
            if (giaohang != null)
            {
                donhang.Tinhtranggiaohang = giaohang;
                data.SubmitChanges();
            }
            var sanpham = from sp in data.CHITIETDONTHANGs where sp.MaDonHang == id select sp;

            return View(sanpham.ToList());

        }
       
    }
}