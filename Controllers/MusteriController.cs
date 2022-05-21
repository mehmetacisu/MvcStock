using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcStokEntities db = new MvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            //var musteriler = db.TblMusteri.ToList();
            var musteriler = db.TblMusteri.Where(x => x.Durum == true).ToList().ToPagedList(sayfa, 3);
            return View(musteriler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TblMusteri musteri)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            else
            {
                musteri.Durum = true;
                db.TblMusteri.Add(musteri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult MusteriSil(TblMusteri musteri)
        {
            var mstr = db.TblMusteri.Find(musteri.Id);
            mstr.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mstr = db.TblMusteri.Find(id);
            return View("MusteriGetir", mstr);

        }

        public ActionResult MusteriGuncelle(TblMusteri musteri)
        {
            var mstr = db.TblMusteri.Find(musteri.Id);
            mstr.Ad = musteri.Ad;
            mstr.Soyad = musteri.Soyad;
            mstr.Sehir = musteri.Sehir;
            mstr.Bakiye = musteri.Bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}