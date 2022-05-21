using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using System.Web.Security;
namespace MvcStok.Controllers
{
    public class GirisController : Controller
    {
        MvcStokEntities db = new MvcStokEntities();
        
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(TblAdmin admin)
        {
            var bilgiler = db.TblAdmin.FirstOrDefault(x=>x.KullaniciAdi == admin.KullaniciAdi && x.Sifre == admin.Sifre);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAdi, false);
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                return View();
            }
        }
    }
}