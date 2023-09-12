namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclasswes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Membership", newName: "Memberships");
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrainingSession",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClassID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Capacity = c.Int(nullable: false),
                        Staff_UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classes", t => t.ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Staff_UserId)
                .Index(t => t.ClassID)
                .Index(t => t.Staff_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingSession", "Staff_UserId", "dbo.Users");
            DropForeignKey("dbo.TrainingSession", "ClassID", "dbo.Classes");
            DropIndex("dbo.TrainingSession", new[] { "Staff_UserId" });
            DropIndex("dbo.TrainingSession", new[] { "ClassID" });
            DropTable("dbo.TrainingSession");
            DropTable("dbo.Classes");
            RenameTable(name: "dbo.Memberships", newName: "Membership");
        }
    }
}
