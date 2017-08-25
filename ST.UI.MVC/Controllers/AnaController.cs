using ST.BLL.Repository;
using ST.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST.UI.MVC.Controllers
{
    public class AnaController : Controller
    {
        // GET: Ana
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Restoranlar()
        {
            var model = new FirmaRepo().GetAll().Where(x => x.AktifMi).Select(x => new FirmaViewModel()
            {
                Id=x.Id,
                Adres=x.Adres,
                EklenmeTarihi=x.EklenmeTarihi,
                FirmaAdi=x.FirmaAdi,
                FirmaKapakFotoPath=x.FirmaKapakFotoPath,
                FirmaProfilFotoPath=x.FirmaProfilFotoParth,
                MinimumSiparisTutari=x.MinimumSiparisTutari,
                OrtalamaTeslimSuresi=x.OrtalamaTeslim,
                Telefon=x.Telefon
            }).OrderBy(x=>x.MinimumSiparisTutari).ThenBy(x=>x.OrtalamaTeslimSuresi).ToList();
            return View(model);
        }
    }
}