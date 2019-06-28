namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableValueForPickupIdFK : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Pickups", newName: "PickupInfoes");
            DropForeignKey("dbo.Customers", "PickupId", "dbo.Pickups");
            DropIndex("dbo.Customers", new[] { "PickupId" });
            AlterColumn("dbo.Customers", "PickupId", c => c.Int());
            CreateIndex("dbo.Customers", "PickupId");
            AddForeignKey("dbo.Customers", "PickupId", "dbo.PickupInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "PickupId", "dbo.PickupInfoes");
            DropIndex("dbo.Customers", new[] { "PickupId" });
            AlterColumn("dbo.Customers", "PickupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "PickupId");
            AddForeignKey("dbo.Customers", "PickupId", "dbo.Pickups", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.PickupInfoes", newName: "Pickups");
        }
    }
}
