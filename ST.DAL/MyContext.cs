﻿using Microsoft.AspNet.Identity.EntityFramework;
using ST.Models.Entities;
using ST.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.DAL
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext()
            : base("name=MyCon")
        {
            //this.RequireUniqueEmail = true;
        }
        public virtual DbSet<Firma> Firmalar { get; set; }
        public virtual DbSet<Urun> Urunler { get; set; }
        public virtual DbSet<UrunKategori> UrunKategoriler { get; set; }
        public virtual DbSet<Adres> Adresler { get; set; }
        public virtual DbSet<FirmaUrun> FirmaUrunleri { get; set; }
        public virtual DbSet<Siparis> Siparisler { get; set; }
        public virtual DbSet<OdemeTipi> OdemeTipleri { get; set; }
        public virtual DbSet<SiparisDetay> SiparisDetaylar { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Fluent API
            modelBuilder.Entity<FirmaUrun>()
                .Property(x => x.UrunFiyat)
                .HasPrecision(7, 2);

            modelBuilder.Entity<SiparisDetay>()
                .Property(x => x.UrunFiyat)
                .HasPrecision(7, 2);
        }
        
    }
}
