namespace gwrsyncronizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "housing.Edids",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        edid = c.String(),
                        strasse = c.String(),
                        eingangnummer = c.String(),
                        plz = c.String(),
                        ort = c.String(),
                        blob = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "housing.Egids",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        egid = c.String(),
                        gem_nr = c.String(),
                        gem_name = c.String(),
                        gb_kreis = c.String(),
                        parz_nr = c.String(),
                        amt_gebnr = c.String(),
                        geb_status = c.String(),
                        geb_kategorie = c.String(),
                        gebFlaeche = c.Int(nullable: false),
                        anz_geschosse = c.Int(nullable: false),
                        baujahr1 = c.String(),
                        baujahr2 = c.String(),
                        renovation1 = c.String(),
                        renovation2 = c.String(),
                        abbruch = c.String(),
                        blob = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "housing.Ewids",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        egid_ewid = c.String(),
                        ewid = c.String(),
                        admin_nr = c.String(),
                        phys_nr = c.String(),
                        stockwerk = c.String(),
                        lage = c.String(),
                        wohnung_status = c.String(),
                        blob = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("housing.Ewids");
            DropTable("housing.Egids");
            DropTable("housing.Edids");
        }
    }
}
