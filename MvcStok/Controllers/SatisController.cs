using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        MvcStokEntities db = new MvcStokEntities();
        public ActionResult Index()
        {
            var satislar = db.TblSatis.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Ürünler
            List<SelectListItem> urun = (from x in db.TblUrun.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Ad,
                                             Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.ddlurun = urun;

            //Personel
            List<SelectListItem> personel = (from x in db.TblPersonel.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Ad +" "+ x.Soyad,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.ddlpersonel = personel;
            //Müşteriler
            List<SelectListItem> musteri = (from x in db.TblMusteri.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.Ad +" "+ x.Soyad,
                                                Value = x.Id.ToString()
                                            }).ToList();
            ViewBag.ddlmusteri = musteri;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TblSatis satis)
        {
            var urun = db.TblUrun.Where(x => x.Id == satis.TblUrun.Id).FirstOrDefault();
            var musteri = db.TblMusteri.Where(x => x.Id == satis.TblMusteri.Id).FirstOrDefault();
            var personel = db.TblPersonel.Where(x => x.Id == satis.TblPersonel.Id).FirstOrDefault();

            satis.TblUrun = urun;
            satis.TblMusteri = musteri;
            satis.TblPersonel = personel;
            satis.Tarih = DateTime.Now;
            db.TblSatis.Add(satis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}