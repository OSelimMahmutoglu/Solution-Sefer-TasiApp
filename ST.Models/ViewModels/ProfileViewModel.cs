﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Models.ViewModels
{
    public class ProfileViewModel
    {
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }
        [Display(Name ="Ad")]
        public string Name { get; set; }
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100,MinimumLength =5,ErrorMessage ="Şifreniz en az 5 karakter olmalıdır")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password",ErrorMessage ="Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Şifreniz en az 5 karakter olmalıdır")]
        [Display(Name = "Eski Şifre")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

    }
}
