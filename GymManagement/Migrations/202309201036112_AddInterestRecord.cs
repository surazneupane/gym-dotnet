namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInterestRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterestRecord",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberID = c.Guid(nullable: false),
                        SessionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InterestRecord");
        }
    }
}
