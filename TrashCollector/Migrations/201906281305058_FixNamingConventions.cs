namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNamingConventions : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PickupInfoes", newName: "Pickups");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Pickups", newName: "PickupInfoes");
        }
    }
}
