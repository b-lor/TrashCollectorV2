namespace TrashCollectorV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedDayofweekModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.DayOfWeeks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DayOfWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
