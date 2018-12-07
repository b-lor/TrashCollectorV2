namespace TrashCollectorV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayofweekFieldChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "DayOfWeek", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "DayOfWeek", c => c.String());
        }
    }
}
