namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigrationss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Memberships", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Memberships", "UserName");
        }
    }
}
