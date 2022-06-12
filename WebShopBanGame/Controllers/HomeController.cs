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

namespace WebShopBanGame.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private SHOPGAMEEntities db = new SHOPGAMEEntities();
        public ActionResult Index(int ?page)
        {
            //var sANPHAMs = db.SANPHAMs.Include(s => s.LOAISANPHAM).Include(s => s.NHACUNGCAP);
            //return View(sANPHAMs.ToList());
            if (page == null) page = 1;            
            var links = (from l in db.SANPHAMs
                         select l).OrderBy(s => s.MaSP);
                       
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
        }
    }
}