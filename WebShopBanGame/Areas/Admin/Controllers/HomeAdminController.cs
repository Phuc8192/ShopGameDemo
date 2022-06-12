using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopBanGame.Models;

namespace WebShopBanGame.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            ViewBag.hoadon = db.HOADONs.Count(x=>x.TrangThai == true);
            ViewBag.doanhthu = db.HOADONs.Where(x=>x.TrangThai == true).ToList().Sum(x => x.CHITIETHOADONs.Sum(n => n.Gia * n.SL));
            ViewBag.khachhang = db.KHACHHANGs.Count();
            ViewBag.sanpham = db.SANPHAMs.Count();
            return View();
        }

        public ActionResult Logoff()
        {
            Session.Remove("TenDNNV");
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dn = new HomeAdminController();
                var kq = dn.KtraDN(model.TenDN, Encryptor.MD5Hash(model.MK));
                if (kq == 1)
                {
                    var user = db.NHANVIENs.Where(x => x.TenDN.Equals(model.TenDN));
                    Session["TenDNNV"] = user.FirstOrDefault().TenDN;
                    Session["MaNV"] = user.FirstOrDefault().MaNV;
                    Session["PQ"] = user.FirstOrDefault().PhanQuyen;
                    //var pq = user.FirstOrDefault().PhanQuyen;
                    //if(pq == false)
                    //{
                    //    Session["PQ"] == (object);
                    //}
                    ModelState.AddModelError("", "Đăng nhập thành công");
                    return RedirectToAction("Index");
                }
                else if (kq == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (kq == -1)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View(model);
        }      

        //public NHANVIEN LayTenDN(string tenDN)
        //{
        //    return db.NHANVIENs.SingleOrDefault(x => x.TenDN == tenDN);
        //}
        //public bool KtraTenDN(string TenDN)
        //{
        //    return db.NHANVIENs.Count(x => x.TenDN == TenDN) > 0;
        //}
        //public bool KtraEmail(string Email)
        //{
        //    return db.NHANVIENs.Count(x => x.Email == Email) > 0;
        //}
        public int KtraDN(string tenDN, string MK)
        {
            var kq = db.NHANVIENs.SingleOrDefault(x => x.TenDN == tenDN);

            if (kq == null)
            {
                return 0;
            }
            else
            {
                if (kq.MK == MK)
                    return 1;
                else
                    return -1;
            }
        }
    }
}