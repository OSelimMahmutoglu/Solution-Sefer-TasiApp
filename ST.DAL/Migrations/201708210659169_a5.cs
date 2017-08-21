namespace ST.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Firmalar", "MinimumSiparisTutari", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Firmalar", "FirmaProfilFotoParth", c => c.String());
            AddColumn("dbo.Firmalar", "FirmaKapakFotoPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Firmalar", "FirmaKapakFotoPath");
            DropColumn("dbo.Firmalar", "FirmaProfilFotoParth");
            DropColumn("dbo.Firmalar", "MinimumSiparisTutari");
        }
    }
}
