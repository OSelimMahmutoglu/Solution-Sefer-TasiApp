namespace ST.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiparisDetay",
                c => new
                    {
                        SiparisId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        Adet = c.Int(nullable: false),
                        UrunFiyat = c.Decimal(nullable: false, precision: 7, scale: 2),
                    })
                .PrimaryKey(t => new { t.SiparisId, t.UrunId })
                .ForeignKey("dbo.Siparisler", t => t.SiparisId, cascadeDelete: true)
                .ForeignKey("dbo.Urunler", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.SiparisId)
                .Index(t => t.UrunId);
            
            AddColumn("dbo.Siparisler", "AdresId", c => c.Int(nullable: false));
            AddColumn("dbo.Siparisler", "FirmaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Siparisler", "AdresId");
            CreateIndex("dbo.Siparisler", "FirmaId");
            AddForeignKey("dbo.Siparisler", "AdresId", "dbo.Adresler", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Siparisler", "FirmaId", "dbo.Firmalar", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiparisDetay", "UrunId", "dbo.Urunler");
            DropForeignKey("dbo.SiparisDetay", "SiparisId", "dbo.Siparisler");
            DropForeignKey("dbo.Siparisler", "FirmaId", "dbo.Firmalar");
            DropForeignKey("dbo.Siparisler", "AdresId", "dbo.Adresler");
            DropIndex("dbo.Siparisler", new[] { "FirmaId" });
            DropIndex("dbo.Siparisler", new[] { "AdresId" });
            DropIndex("dbo.SiparisDetay", new[] { "UrunId" });
            DropIndex("dbo.SiparisDetay", new[] { "SiparisId" });
            DropColumn("dbo.Siparisler", "FirmaId");
            DropColumn("dbo.Siparisler", "AdresId");
            DropTable("dbo.SiparisDetay");
        }
    }
}
