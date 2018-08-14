namespace gwrsyncronizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferencesAndHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("housing.Edids", "guid", c => c.String());
            AddColumn("housing.Edids", "guid_egid", c => c.String());
            AddColumn("housing.Edids", "valid_from", c => c.DateTime(nullable: false));
            AddColumn("housing.Edids", "valid_now", c => c.Int(nullable: false));
            AddColumn("housing.Edids", "created_user", c => c.String());
            AddColumn("housing.Edids", "created_at", c => c.DateTime(nullable: false));
            AddColumn("housing.Edids", "modified_user", c => c.String());
            AddColumn("housing.Edids", "modified_at", c => c.DateTime(nullable: false));
            AddColumn("housing.Egids", "guid", c => c.String());
            AddColumn("housing.Egids", "valid_from", c => c.DateTime(nullable: false));
            AddColumn("housing.Egids", "valid_now", c => c.Int(nullable: false));
            AddColumn("housing.Egids", "created_user", c => c.String());
            AddColumn("housing.Egids", "created_at", c => c.DateTime(nullable: false));
            AddColumn("housing.Egids", "modified_user", c => c.String());
            AddColumn("housing.Egids", "modified_at", c => c.DateTime(nullable: false));
            AddColumn("housing.Ewids", "guid", c => c.String());
            AddColumn("housing.Ewids", "guid_egid", c => c.String());
            AddColumn("housing.Ewids", "valid_from", c => c.DateTime(nullable: false));
            AddColumn("housing.Ewids", "valid_now", c => c.Int(nullable: false));
            AddColumn("housing.Ewids", "created_user", c => c.String());
            AddColumn("housing.Ewids", "created_at", c => c.DateTime(nullable: false));
            AddColumn("housing.Ewids", "modified_user", c => c.String());
            AddColumn("housing.Ewids", "modified_at", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("housing.Ewids", "modified_at");
            DropColumn("housing.Ewids", "modified_user");
            DropColumn("housing.Ewids", "created_at");
            DropColumn("housing.Ewids", "created_user");
            DropColumn("housing.Ewids", "valid_now");
            DropColumn("housing.Ewids", "valid_from");
            DropColumn("housing.Ewids", "guid_egid");
            DropColumn("housing.Ewids", "guid");
            DropColumn("housing.Egids", "modified_at");
            DropColumn("housing.Egids", "modified_user");
            DropColumn("housing.Egids", "created_at");
            DropColumn("housing.Egids", "created_user");
            DropColumn("housing.Egids", "valid_now");
            DropColumn("housing.Egids", "valid_from");
            DropColumn("housing.Egids", "guid");
            DropColumn("housing.Edids", "modified_at");
            DropColumn("housing.Edids", "modified_user");
            DropColumn("housing.Edids", "created_at");
            DropColumn("housing.Edids", "created_user");
            DropColumn("housing.Edids", "valid_now");
            DropColumn("housing.Edids", "valid_from");
            DropColumn("housing.Edids", "guid_egid");
            DropColumn("housing.Edids", "guid");
        }
    }
}
