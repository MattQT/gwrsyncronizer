namespace gwrsyncronizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("housing.Edids", "guid_egid_reference", c => c.String());
            AddColumn("housing.Ewids", "guid_edid_reference", c => c.String());
            DropColumn("housing.Edids", "guid_egid");
            DropColumn("housing.Ewids", "guid_egid");
        }
        
        public override void Down()
        {
            AddColumn("housing.Ewids", "guid_egid", c => c.String());
            AddColumn("housing.Edids", "guid_egid", c => c.String());
            DropColumn("housing.Ewids", "guid_edid_reference");
            DropColumn("housing.Edids", "guid_egid_reference");
        }
    }
}
