namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedaddressandcontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Users", "ContactNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ContactNumber");
            DropColumn("dbo.Users", "Address");
        }
    }
}
