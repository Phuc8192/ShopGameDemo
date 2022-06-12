using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopBanGame.Models;
using PagedList;

namespace WebShopBanGame.Areas.Admin.Controllers
{
    public class QLHoaDonsController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();

        // GET: Admin/QLHoaDons
        public ActionResult Index(int? page)
        {
            //var hOADONs = db.HOADONs.Include(h => h.KHACHHANG);
            //return View(hOADONs.ToList());
            if (page == null) page = 1;
            var links = (from l in db.HOADONs
                         select l).OrderByDescending(s => s.MaHD);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/QLHoaDons/Details/5
        public ActionResult Details(string id)
        {
            List<CHITIETHOADON> _cthd = db.CHITIETHOADONs.Where(x => x.MaHD == id).ToList();
            return View(_cthd);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //HOADON hOADON = db.HOADONs.Find(id);
            //if (hOADON == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(hOADON);
        }

        // GET: Admin/QLHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH");
            return View();
        }

        // POST: Admin/QLHoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaKH,NgayBan,TrangThai")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.HOADONs.Add(hOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", hOADON.MaKH);
            return View(hOADON);
        }

        // GET: Admin/QLHoaDons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", hOADON.MaKH);
            return View(hOADON);
        }

        // POST: Admin/QLHoaDons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaKH,NgayBan,TrangThai")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", hOADON.MaKH);
            return View(hOADON);
        }

        // GET: Admin/QLHoaDons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // POST: Admin/QLHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //DSCDKEY dSCDKEY = db.DSCDKEYs.Find(id);
            CHITIETHOADON cTHD = db.CHITIETHOADONs.Find(id);
            List<DSCDKEY> _cdkey = db.DSCDKEYs.Where(x => x.MaHD == id).ToList();
            int i = _cdkey.Count;
            //List<CHITIETHOADON> _cthd = db.CHITIETHOADONs.Where(x => x.MaHD == id).ToList();            
            HOADON hOADON = db.HOADONs.Find(id);
            //for(int j=0;j<=i;j++)
            //{
            //    db.DSCDKEYs.Remove(dSCDKEY);
            //}
            
            db.CHITIETHOADONs.Remove(cTHD);
            db.HOADONs.Remove(hOADON);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DuyetHD(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            hOADON.TrangThai = true;
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
