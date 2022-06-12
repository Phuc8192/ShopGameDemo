using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using WebShopBanGame.Models;
using System.Data.Entity.Validation;

namespace WebShopBanGame.Controllers
{
    public class UserController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();

        string LayMaKH()
        {
            var maMax = db.KHACHHANGs.ToList().Select(n => n.MaKH).Max();
            int maKH = int.Parse(maMax.Substring(2)) + 1;
            string KH = String.Concat("00", maKH.ToString());
            return "KH" + KH.Substring(maKH.ToString().Length - 1);
        }
        // GET: User       
        
        public ActionResult UserInfo()
        {
            return View();
        }
        
                

        [HttpPost]
        public ActionResult ResultLogin()
        {

            return View();
        }

        public ActionResult LoginDemo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginDemo(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var đn = new UserController();
                var kq = đn.KtraDN(model.TenDN, model.MK);
                if(kq == 1)
                {
                    var user = đn.LayTenDN(model.TenDN);
                    ModelState.AddModelError("", "Đăng nhập thành công");
                    return Redirect("/");
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
       
        public ActionResult Logoff()
        {
            Session.Clear();
            return Redirect("/");
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
                var dn = new UserController();
                var kq = dn.KtraDN(model.TenDN, Encryptor.MD5Hash(model.MK));
                if (kq == 1)
                {
                    var user = db.KHACHHANGs.Where(x => x.TenDN.Equals(model.TenDN));
                    Session["TenDN"] = user.FirstOrDefault().TenDN;
                    Session["MaKH"] = user.FirstOrDefault().MaKH;              
                    ModelState.AddModelError("", "Đăng nhập thành công");
                    return Redirect("/");
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

        [HttpGet]
        public ActionResult RegisterDemo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterDemo(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var ktra = new UserController();
                if(ktra.KtraTenDN(model.TenDN))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if(ktra.KtraEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var KH = new KHACHHANG();
                    KH.MaKH = LayMaKH();
                    KH.TenKH = model.TenKH;
                    KH.TenDN = model.TenDN;
                    KH.Email = model.Email;
                    KH.MK = model.MK;
                    db.KHACHHANGs.Add(KH);
                    db.SaveChanges();
                    return Redirect("Login");
                }
            }           
            return View(model);
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var ktra = new UserController();
                if (ktra.KtraTenDN(model.TenDN))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (ktra.KtraEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var KH = new KHACHHANG();
                    KH.MaKH = LayMaKH();
                    KH.TenKH = model.TenKH;
                    KH.TenDN = model.TenDN;
                    KH.Email = model.Email;
                    KH.MK = Encryptor.MD5Hash(model.MK);
                    db.KHACHHANGs.Add(KH);
                    try
                    {
                        db.SaveChanges();
                        ViewBag.Success = "Đăng ký thành công";
                        return RedirectToAction("Login");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }                    
                }
            }
            return View(model);
        }

        public KHACHHANG LayTenDN(string tenDN)
        {
            return db.KHACHHANGs.SingleOrDefault(x => x.TenDN == tenDN);
        }
        public bool KtraTenDN(string TenDN)
        {
            return db.KHACHHANGs.Count(x => x.TenDN == TenDN) > 0;
        }
        public bool KtraEmail(string Email)
        {
            return db.KHACHHANGs.Count(x => x.Email == Email) > 0;
        }
        public int KtraDN(string tenDN, string MK, bool isLoginAdmin = false)
        {
            var kq = db.KHACHHANGs.SingleOrDefault(x => x.TenDN == tenDN);
            
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
        //[HttpGet]
        //public ActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register([Bind(Include = "MaKH,TenKH,SDT,Email,TenDN,MK,Avatar")] KHACHHANG kHACHHANG)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        kHACHHANG.MaKH = "2001";
        //        kHACHHANG.SDT = null;
        //        kHACHHANG.Avatar = null;
        //        db.KHACHHANGs.Add(kHACHHANG);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(kHACHHANG);
        //}
        public ActionResult Password()
        {
            return View();
        }
    }
}