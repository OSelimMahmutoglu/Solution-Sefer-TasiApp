using Microsoft.AspNet.Identity;
using ST.BLL.Repository;
using ST.BLL.Settings;
using ST.Models.Entities;
using ST.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ST.UI.MVC.Controllers
{
    [Authorize(Roles = "Firma")]
    public class FirmaController : Controller
    {
        //[AllowAnonymous] herkese açık alan
        public ActionResult Index()
        {
            var firma = new FirmaRepo().GetByUserId(HttpContext.User.Identity.GetUserId());
            FirmaViewModel model = null;
            if (firma != null)
                model = new FirmaViewModel()
                {
                    Id = firma.Id,
                    KullaniciId = firma.KullaniciId,
                    Adres = firma.Adres,
                    AktifMi = firma.AktifMi,
                    EklenmeTarihi = firma.EklenmeTarihi,
                    FirmaAdi = firma.FirmaAdi,
                    FirmaKapakFotoPath = firma.FirmaKapakFotoPath,
                    FirmaProfilFotoParth = firma.FirmaProfilFotoParth,
                    MinimumSiparisTutari = firma.MinimumSiparisTutari,
                    OrtalamaTeslimSuresi = firma.OrtalamaTeslim,
                    Telefon = firma.Telefon,
                    WebUrl = firma.WebUrl
                };
            return View(model);
        }

        public ActionResult Ekle()
        {
            var firma = new FirmaRepo().GetByUserId(HttpContext.User.Identity.GetUserId());
            if (firma != null)
            {
                ViewBag.durum = false;
                ViewBag.mesaj = "Zaten 1 adet firmanız var. Yeni firma ekleyemezsiniz!";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(FirmaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var firma = new Firma()
                {
                    KullaniciId = HttpContext.User.Identity.GetUserId(),
                    Adres = model.Adres,
                    AktifMi = model.AktifMi,
                    EklenmeTarihi = model.EklenmeTarihi,
                    FirmaAdi = model.FirmaAdi,
                    MinimumSiparisTutari = model.MinimumSiparisTutari,
                    OrtalamaTeslim = model.OrtalamaTeslimSuresi,
                    Telefon = model.Telefon,
                    WebUrl = model.WebUrl
                };
                new FirmaRepo().Insert(firma);
                if (model.FirmaProfilFotoFile != null && model.FirmaProfilFotoFile.ContentLength > 0)
                {
                    var file = model.FirmaProfilFotoFile;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extName = Path.GetExtension(file.FileName);
                    fileName = fileName?.Replace(" ", "");
                    fileName += Guid.NewGuid().ToString().Replace("-", "");
                    fileName = SiteSettings.UrlFormatConver(fileName);
                    var klasorYolu = Server.MapPath("~/Upload/" + firma.Id);
                    var dosyaYolu = Server.MapPath("~/Upload/" + firma.Id + "/") + fileName + extName;
                    if (!Directory.Exists(klasorYolu))
                        Directory.CreateDirectory(klasorYolu);
                    file.SaveAs(dosyaYolu);
                    WebImage img = new WebImage(dosyaYolu);
                    img.Resize(240, 140, false);
                    img.AddTextWatermark("Sefer Tası - BAU", "Tomato", opacity: 75, fontSize: 16, fontFamily: "Verdana", horizontalAlign: "Left");
                    img.Save(dosyaYolu);
                    var ff = new FirmaRepo().GetByID(firma.Id);
                    ff.FirmaProfilFotoParth = $"Upload/{firma.Id}/{fileName}";
                    new FirmaRepo().Update();
                }
                if (model.FirmaProfilFotoParth != null && model.FirmaKapakFotoFile.ContentLength > 0)
                {
                    var file = model.FirmaProfilFotoFile;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extName = Path.GetExtension(file.FileName);
                    fileName = fileName?.Replace(" ", "");
                    fileName += Guid.NewGuid().ToString().Replace("-", "");
                    fileName = SiteSettings.UrlFormatConver(fileName);
                    var klasorYolu = Server.MapPath("~/Upload/" + firma.Id);
                    var dosyaYolu = Server.MapPath("~/Upload/" + firma.Id + "/") + fileName + extName;
                    if (!Directory.Exists(klasorYolu))
                        Directory.CreateDirectory(klasorYolu);
                    file.SaveAs(dosyaYolu);
                    WebImage img = new WebImage(dosyaYolu);
                    img.Resize(1670, 480, false);
                    img.AddTextWatermark("Sefer Tası - BAU", "Tomato", opacity: 75, fontSize: 16, fontFamily: "Verdana", horizontalAlign: "Left");
                    img.Save(dosyaYolu);
                    var ff = new FirmaRepo().GetByID(firma.Id);
                    ff.FirmaProfilFotoParth = $"Upload/{firma.Id}/{fileName}";
                    new FirmaRepo().Update();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

    }
}