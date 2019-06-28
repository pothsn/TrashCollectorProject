namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakingExtraPickupAndTemporarySuspentionNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pickups", "ExtraPickupDay", c => c.DateTime());
            AlterColumn("dbo.Pickups", "TemporarySuspensionStart", c => c.DateTime());
            AlterColumn("dbo.Pickups", "TemporarySuspensionEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pickups", "TemporarySuspensionEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pickups", "TemporarySuspensionStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pickups", "ExtraPickupDay", c => c.DateTime(nullable: false));
        }
    }
}
