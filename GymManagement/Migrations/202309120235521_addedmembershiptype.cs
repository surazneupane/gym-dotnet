namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmembershiptype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Membership",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberID = c.Int(nullable: false),
                        MembershipTypeID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        Member_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Member_UserId)
                .ForeignKey("dbo.MembershipTypes", t => t.MembershipTypeID, cascadeDelete: true)
                .Index(t => t.MembershipTypeID)
                .Index(t => t.Member_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Membership", "MembershipTypeID", "dbo.MembershipTypes");
            DropForeignKey("dbo.Membership", "Member_UserId", "dbo.Users");
            DropIndex("dbo.Membership", new[] { "Member_UserId" });
            DropIndex("dbo.Membership", new[] { "MembershipTypeID" });
            DropTable("dbo.Membership");
            DropTable("dbo.MembershipTypes");
        }
    }
}
