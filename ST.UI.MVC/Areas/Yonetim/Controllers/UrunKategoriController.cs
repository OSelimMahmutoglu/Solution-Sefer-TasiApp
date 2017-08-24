using ST.BLL.Repository;
using ST.Models.Entities;
using ST.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST.UI.MVC.Areas.Yonetim.Controllers
{
    public class UrunKategoriController : Controller
    {
        // GET: Yonetim/UrunKategori
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Ekle(UrunKategoriViewModel model)
        {
            try
            {
                new UrunKategoriRepo().
                    Insert(new UrunKategori()
                    {
                        KategoriAdi = model.KategoriAdi,
                        Aciklama = model.Aciklama
                    });
                var data = new
                {
                    succes = true,
                    message = "Kategori ekleme işlemi başarılı"
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new
                {
                    succes = false,
                    message = "Kategori ekleme işlemi başarısız:" + ex.Message
                };
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Getir()
        {
            var model = new UrunKategoriRepo().GetAll().OrderBy(x => x.KategoriAdi).Select(x => new UrunKategoriViewModel()
            {
                Id = x.Id,
                Aciklama = x.Aciklama,
                KategoriAdi = x.KategoriAdi
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Guncelle(UrunKategoriViewModel model)
        {
            try
            {
                var kategori = new UrunKategoriRepo().GetByID(model.Id);
                kategori.KategoriAdi = model.KategoriAdi;
                kategori.Aciklama = model.Aciklama;
                new UrunKategoriRepo().Update();
                var data = new
                {
                    success = true,
                    message = "Kategori Güncelleme işlemi başarılı"
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new
                {
                    success = false,
                    message = "Kategori Güncelleme İşlemi başarısız:" + ex.Message
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpDelete]
        public JsonResult Sil(int? id)
        {
            try
            {
                var kategori = new UrunKategoriRepo().GetByID(id.Value);
                new UrunKategoriRepo().Delete(kategori);
                var data = new
                {
                    success = true,
                    message = "Kategori Silme İşlemi başarılı"
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new
                {
                    success = false,
                    message = "kategori silinemedi:" + ex.Message
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}