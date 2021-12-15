using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {

        MvcStokEntities db = new MvcStokEntities();
        public ActionResult Index(string ara)
        {
            var urun = from x in db.TblUrun where x.Durum==true select x;
            if (!string.IsNullOrEmpty(ara))
            {
                urun = urun.Where(x => x.Ad.Contains(ara) && x.Durum ==true);
            }
            return View(urun.ToList());
        }


        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.TblKategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Ad,
                                            Value = x.Id.ToString()
                                        }).ToList();
            ViewBag.kategori = ktg;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TblUrun urun)
        {
            var ktgr = db.TblKategori.Where(x => x.Id == urun.TblKategori.Id).FirstOrDefault();
            urun.TblKategori = ktgr;
            db.TblUrun.Add(urun);
            db.SaveChanges();
            return RedirectToAction("Index", "Urun");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kategorisi = (from x in db.TblKategori.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Ad,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.urunktg = kategorisi;
            var urun = db.TblUrun.Find(id);
            return View("UrunGetir", urun);
        }

        public ActionResult UrunGuncelle(TblUrun urun)
        {
            var ktgr = db.TblKategori.Where(x => x.Id == urun.TblKategori.Id).FirstOrDefault();
            var urn = db.TblUrun.Find(urun.Id);
            urn.Ad = urun.Ad;
            urn.Marka = urun.Marka;
            urn.Stok = urun.Stok;
            urn.AlisFiyat = urun.AlisFiyat;
            urn.SatisFiyat = urun.SatisFiyat;
            urn.Kategori = ktgr.Id;
            db.SaveChanges();
            return RedirectToAction("Index","Urun");
        }

        public ActionResult UrunSil(TblUrun urun)
        {
            var urn = db.TblUrun.Find(urun.Id);
            urn.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}