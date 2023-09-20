namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcheckin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberID = c.Guid(nullable: false),
                        CheckInDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Attendance");
        }
    }
}
