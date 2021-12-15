using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcStokEntities db = new MvcStokEntities();
        public ActionResult Index()
        {
            var kategori = db.TblKategori.ToList();
            return View(kategori);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]

        public ActionResult YeniKategori(TblKategori kategori)
        {
            db.TblKategori.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index","Kategori");
        }


        public ActionResult Sil(int id)
        {
            var kategori = db.TblKategori.Find(id);
            db.TblKategori.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }

        public ActionResult Guncelle(int id)
        {
            var kategori = db.TblKategori.Find(id);
            return View("Guncelle", kategori);
        }

        public ActionResult KategoriGuncelle(TblKategori kategori)
        {
            var ktg = db.TblKategori.Find(kategori.Id);
            ktg.Ad = kategori.Ad;
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }
    }
}