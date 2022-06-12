using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopBanGame.Models;

namespace WebShopBanGame.Controllers
{
    public class CHITIETHOADONsController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();

        // GET: CHITIETHOADONs
        public ActionResult Index(string id)
        {
            List<CHITIETHOADON> _cthd = db.CHITIETHOADONs.Where(x => x.MaHD == id).ToList();
            return View(_cthd);
        }

        // GET: CHITIETHOADONs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADONs/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH");
            ViewBag.MaNV = new SelectList(db.NHANVIENs, "MaNV", "TenNV");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP");
            return View();
        }

        // POST: CHITIETHOADONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSP,MaNV,Gia,SL")] CHITIETHOADON cHITIETHOADON)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETHOADONs.Add(cHITIETHOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cHITIETHOADON.MaHD);
            ViewBag.MaNV = new SelectList(db.NHANVIENs, "MaNV", "TenNV", cHITIETHOADON.MaNV);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cHITIETHOADON.MaSP);
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADONs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cHITIETHOADON.MaHD);
            ViewBag.MaNV = new SelectList(db.NHANVIENs, "MaNV", "TenNV", cHITIETHOADON.MaNV);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cHITIETHOADON.MaSP);
            return View(cHITIETHOADON);
        }

        // POST: CHITIETHOADONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSP,MaNV,Gia,SL")] CHITIETHOADON cHITIETHOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETHOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cHITIETHOADON.MaHD);
            ViewBag.MaNV = new SelectList(db.NHANVIENs, "MaNV", "TenNV", cHITIETHOADON.MaNV);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cHITIETHOADON.MaSP);
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADONs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHOADON);
        }

        // POST: CHITIETHOADONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            db.CHITIETHOADONs.Remove(cHITIETHOADON);
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
