namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedNamingOfPropertiesForNewDateTimeChangesInPickupModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "ExtraPickupDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "TemporarySuspensionStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "TemporarySuspensionEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.Pickups", "ExtraPickup");
            DropColumn("dbo.Pickups", "SuspendStart");
            DropColumn("dbo.Pickups", "SuspendEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "SuspendEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "SuspendStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "ExtraPickup", c => c.DateTime(nullable: false));
            DropColumn("dbo.Pickups", "TemporarySuspensionEnd");
            DropColumn("dbo.Pickups", "TemporarySuspensionStart");
            DropColumn("dbo.Pickups", "ExtraPickupDay");
        }
    }
}
