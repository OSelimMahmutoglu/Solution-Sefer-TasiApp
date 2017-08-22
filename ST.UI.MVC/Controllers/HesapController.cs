using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ST.BLL.Account;
using ST.Models.IdentityModels;
using ST.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ST.BLL.Account.MembershipTools;

namespace ST.UI.MVC.Controllers
{
    public class HesapController : Controller
    {
        // GET: Hesap
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Kayit(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userManager = NewUserManager();
            var checkUser = userManager.FindByEmail(model.Email);
            if (checkUser != null)
            {
                ModelState.AddModelError("", "Bu email adresi kullanılmaktadır");
                return View(model);
            }
            checkUser = await userManager.FindByNameAsync(model.Username);
            if (checkUser != null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı kullanılmaktadır");
                return View(model);
            }
            var user = new ApplicationUser()
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Surname = model.Surname,
                UserName = model.Username,

            };
            bool adminMi = userManager.Users.Count() == 0;
            var sonuc = await userManager.CreateAsync(user, model.Password);
            if (sonuc.Succeeded)
            {
                if (adminMi)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
                else
                {
                    if (model.FirmaMi)
                        userManager.AddToRole(user.Id, "Firma");
                    else
                        userManager.AddToRole(user.Id, "Musteri");
                }
                return RedirectToAction("Giris", "Hesap");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı kayıt işleminde hata oluştu!");
                return View();

            }

        }

        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Giris(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userManager = NewUserManager();
            var user = await userManager.FindAsync(model.NameEmail, model.Password);
            if (user == null)
            {
                var emailuser = await userManager.FindByEmailAsync(model.NameEmail);
                if (emailuser == null)
                {
                    ModelState.AddModelError(string.Empty, "Böyle bir kullanıcı bulunamadı!");
                    return View(model);
                }
                user = await userManager.FindAsync(emailuser.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı");
                    return View(model);
                }
                else
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = await userManager.CreateIdentityAsync(user,DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(new AuthenticationProperties() {
                    IsPersistent=model.RememberMe
                    },userIdentity);
                }
            }
            else
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties()
                {
                    IsPersistent = model.RememberMe
                }, userIdentity);
            }
         
            return RedirectToAction("Index","Ana");
        }
        [Authorize] //Giriş yapanlar görebilir
        public ActionResult Cikis()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index","Ana");
        }
        
    }
}