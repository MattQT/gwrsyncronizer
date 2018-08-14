namespace gwrsyncronizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameProperty : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "housing.Egids", name: "gebFlaeche", newName: "geb_flaeche");
        }
        
        public override void Down()
        {
            RenameColumn(table: "housing.Egids", name: "geb_flaeche", newName: "gebFlaeche");
        }
    }
}
