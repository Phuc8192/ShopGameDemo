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
    public class DSCDKEYsController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();

        // GET: DSCDKEYs
        public ActionResult Index(string id)
        {
            List<DSCDKEY> _cdkey = db.DSCDKEYs.Where(x => x.MaHD == id).ToList();
            return View(_cdkey);
        }

        // GET: DSCDKEYs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSCDKEY dSCDKEY = db.DSCDKEYs.Find(id);
            if (dSCDKEY == null)
            {
                return HttpNotFound();
            }
            return View(dSCDKEY);
        }

        // GET: DSCDKEYs/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP");
            return View();
        }

        // POST: DSCDKEYs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSP,CDKey")] DSCDKEY dSCDKEY)
        {
            if (ModelState.IsValid)
            {
                db.DSCDKEYs.Add(dSCDKEY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", dSCDKEY.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", dSCDKEY.MaSP);
            return View(dSCDKEY);
        }

        // GET: DSCDKEYs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSCDKEY dSCDKEY = db.DSCDKEYs.Find(id);
            if (dSCDKEY == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", dSCDKEY.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", dSCDKEY.MaSP);
            return View(dSCDKEY);
        }

        // POST: DSCDKEYs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSP,CDKey")] DSCDKEY dSCDKEY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dSCDKEY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", dSCDKEY.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", dSCDKEY.MaSP);
            return View(dSCDKEY);
        }

        // GET: DSCDKEYs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSCDKEY dSCDKEY = db.DSCDKEYs.Find(id);
            if (dSCDKEY == null)
            {
                return HttpNotFound();
            }
            return View(dSCDKEY);
        }

        // POST: DSCDKEYs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DSCDKEY dSCDKEY = db.DSCDKEYs.Find(id);
            db.DSCDKEYs.Remove(dSCDKEY);
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
