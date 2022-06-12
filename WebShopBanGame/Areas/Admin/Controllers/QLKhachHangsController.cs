using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopBanGame.Models;

namespace WebShopBanGame.Areas.Admin.Controllers
{
    public class QLKhachHangsController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();

        // GET: Admin/QLKhachHangs
        public ActionResult Index()
        {
            ViewBag.hoadon = db.HOADONs.Count(x => x.TrangThai == true);
            return View(db.KHACHHANGs.ToList());
        }

        // GET: Admin/QLKhachHangs/Details/5
        public ActionResult Details(string id)
        {
            List<HOADON> _hd = db.HOADONs.Where(x => x.MaKH == id).ToList();
            Session["_cthd"] = id;
            return View(_hd);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            //if (kHACHHANG == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(kHACHHANG);
        }

        public ActionResult XemCTHD(string id)
        {
            List<CHITIETHOADON> _cthd = db.CHITIETHOADONs.Where(x => x.MaHD == id).ToList();
            return View(_cthd);
        }

        // GET: Admin/QLKhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLKhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,SDT,Email,TenDN,MK,Avatar,SoDu")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHACHHANG);
        }

        // GET: Admin/QLKhachHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/QLKhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,SDT,Email,TenDN,MK,Avatar,SoDu")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }

        // GET: Admin/QLKhachHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/QLKhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            db.KHACHHANGs.Remove(kHACHHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
