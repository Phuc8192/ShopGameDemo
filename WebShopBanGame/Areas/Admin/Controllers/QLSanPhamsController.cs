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
    public class QLSanPhamsController : Controller
    {
        private SHOPGAMEEntities db = new SHOPGAMEEntities();

        // GET: Admin/QLSanPhams
        public ActionResult Index() //int ?page, string sapxep
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.LOAISANPHAM).Include(s => s.NHACUNGCAP);
            return View(sANPHAMs.ToList());
            //if (page == null) page = 1;
            ////var links = (from l in db.SANPHAMs
            ////             select l).OrderByDescending(s => s.MaSP);

            //ViewBag.SXTen = String.IsNullOrEmpty(sapxep) ? "tenSP" : "";

            //var links = from l in db.SANPHAMs select l;           

            //switch (sapxep)
            //{
            //    case "tenSP":
            //        links = links.OrderBy(s => s.TenSP); //sắp sếp z - a
            //        break;
            //    default:
            //        links = links.OrderByDescending(s => s.MaSP);
            //        break;
            //}

            //int pageSize = 16;
            //int pageNumber = (page ?? 1);
            //return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/QLSanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/QLSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs, "MaLSP", "TenLSP");
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC");
            return View();
        }

        // POST: Admin/QLSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Gia,MaLSP,MaNCC,TieuDe,NoiDung,Anh1,Anh2,Anh3,Anh4")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs, "MaLSP", "TenLSP", sANPHAM.MaLSP);
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            return View(sANPHAM);
        }

        // GET: Admin/QLSanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs, "MaLSP", "TenLSP", sANPHAM.MaLSP);
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            return View(sANPHAM);
        }

        // POST: Admin/QLSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,Gia,MaLSP,MaNCC,TieuDe,NoiDung,Anh1,Anh2,Anh3,Anh4")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs, "MaLSP", "TenLSP", sANPHAM.MaLSP);
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", sANPHAM.MaNCC);
            return View(sANPHAM);
        }

        // GET: Admin/QLSanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/QLSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
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
