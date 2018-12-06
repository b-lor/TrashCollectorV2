namespace TrashCollectorV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedExtraDBField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "UserId", c => c.String());
        }
    }
}
