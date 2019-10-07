using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_1D2C.Models;
using Web_1D2C.VNPAY;

namespace Web_1D2C.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbQLBangiayDataContext data = new dbQLBangiayDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
          public ActionResult ThemGiohang(int iMasp, string strURL)
        {
             //Lay ra Session gio hang
             List<Giohang> lstGiohang = Laygiohang();
             //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
             Giohang sanpham = lstGiohang.Find(n => n.iMasp == iMasp);
                 if(sanpham==null)
                 {
                     sanpham=new Giohang(iMasp);
                     lstGiohang.Add(sanpham);
                     return Redirect(strURL);             
                 }
                 else
                 {
                     sanpham.iSoluong++;
                     return Redirect(strURL);
                 }
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Shoes");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {

            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult XoaGiohang(int iMaSP)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMasp == iMaSP);
                return RedirectToAction("GioHang");

            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Shoes");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Shoes");
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Shoes");
            }

            

            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();


            // sinh cac url thanh toan online
            Web_1D2C.VNPAY.VNPAY vnpay = new Web_1D2C.VNPAY.VNPAY();
            foreach (var bank in vnpay.ListBank)
            {
                // an gian tao ma hoa don, 1s ko duoc sinh ra 2 don hang
                bank.Url = vnpay.GetBankingURL(ViewBag.Tongtien, bank.Code, DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now);
            }
            Session["vnpay"] = vnpay;

            return View(lstGiohang);
        }

        // nhan ket qua thanh toan online
        [HttpGet]
        public ActionResult KetQuaThanhToanOnline()
        {
            if (Request.QueryString.Count > 0)
            {
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();
                //if (vnpayData.Count > 0)
                //{
                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                // }

                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                string orderId = vnpay.GetResponseData("vnp_TxnRef");

                // lấy ra idkiosk để lấy hashsecret trong kiosk
                Web_1D2C.VNPAY.VNPAY vnpay1 = new Web_1D2C.VNPAY.VNPAY();
                string vnp_HashSecret = vnpay1.VNPAY_HASH_SECRECT;


               

                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                //vnp_SecureHash: MD5 cua du lieu tra ve
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];


                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {

                    if (vnp_ResponseCode.Equals("00"))
                    {
                        //Thanh toan thanh cong
                        ViewBag.Message = "Thanh toán thành công";

                        //Them Don hang
                        DONDATHANG ddh = new DONDATHANG();
                        KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
                        List<Giohang> gh = Laygiohang();
                        ddh.MaKH = kh.MaKH;
                        ddh.Ngaydat = DateTime.Now;
                       //var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
                        ddh.Ngaygiao = DateTime.Now;
                        ddh.Tinhtranggiaohang = false;
                        ddh.Dathanhtoan = true;
                        data.DONDATHANGs.InsertOnSubmit(ddh);
                        data.SubmitChanges();
                        //Them chi tiet don hang            
                        foreach (var item in gh)
                        {
                            CHITIETDONTHANG ctdh = new CHITIETDONTHANG();
                            ctdh.MaDonHang = ddh.MaDonHang;
                            ctdh.Masp = item.iMasp;
                            ctdh.Soluong = item.iSoluong;
                            ctdh.Dongia = (decimal)item.dDongia;
                            data.CHITIETDONTHANGs.InsertOnSubmit(ctdh);
                        }
                        data.SubmitChanges();
                        Session["Giohang"] = null;


                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;

                        

                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                    
                }

                return View();
            }

            return HttpNotFound();
        }

        //Xay dung chuc nang Dathang
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.Ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(ngaygiao);
            ddh.Tinhtranggiaohang = false;
            ddh.Dathanhtoan = false;
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            //Them chi tiet don hang            
            foreach (var item in gh)
            {
                CHITIETDONTHANG ctdh = new CHITIETDONTHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.Masp = item.iMasp;
                ctdh.Soluong = item.iSoluong;
                ctdh.Dongia = (decimal)item.dDongia;
                data.CHITIETDONTHANGs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }




        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}