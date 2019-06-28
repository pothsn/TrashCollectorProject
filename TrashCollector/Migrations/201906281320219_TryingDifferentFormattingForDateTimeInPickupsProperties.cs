namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingDifferentFormattingForDateTimeInPickupsProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "ExtraPickup", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "SuspendStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "SuspendEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.Pickups", "ExtraPickupDay");
            DropColumn("dbo.Pickups", "TemporarySuspensionStart");
            DropColumn("dbo.Pickups", "TemporarySuspensionEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "TemporarySuspensionEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "TemporarySuspensionStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Pickups", "ExtraPickupDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Pickups", "SuspendEnd");
            DropColumn("dbo.Pickups", "SuspendStart");
            DropColumn("dbo.Pickups", "ExtraPickup");
        }
    }
}
