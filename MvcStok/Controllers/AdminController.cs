using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class AdminController : Controller
    {
        MvcStokEntities db = new MvcStokEntities();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(TblAdmin admin)
        {
            db.TblAdmin.Add(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}