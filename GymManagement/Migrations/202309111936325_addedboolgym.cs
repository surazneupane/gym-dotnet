namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedboolgym : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsGymStaff", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsGymStaff");
        }
    }
}
