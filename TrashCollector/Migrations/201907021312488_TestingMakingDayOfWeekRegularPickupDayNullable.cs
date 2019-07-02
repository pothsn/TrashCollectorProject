namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestingMakingDayOfWeekRegularPickupDayNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pickups", "RegularPickupDay", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pickups", "RegularPickupDay", c => c.Int(nullable: false));
        }
    }
}
