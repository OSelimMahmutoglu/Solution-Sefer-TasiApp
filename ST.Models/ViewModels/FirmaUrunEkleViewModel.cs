using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ST.Models.ViewModels
{
   public class FirmaUrunEkleViewModel
    {
        public UrunKategoriViewModel UrunKategori { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }
        public string UrunFotografYolu { get; set; } //Göstermek için
        public int UrunKategoriId { get; set; }
        public decimal Fiyat { get; set; } = 0;
        public int FirmaId { get; set; }
        [Display(Name = "Satışta Mı?")]
        public bool SatistaMi { get; set; }

        public HttpPostedFileBase UrunFotografFile { get; set; } //Yüklemek için


    }
}
