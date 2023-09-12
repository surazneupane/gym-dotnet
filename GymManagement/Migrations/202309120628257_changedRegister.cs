namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class changedRegister : DbMigration
    {
        public override void Up()
        {
            // Assuming Membership table has a column called NewMemberID which will hold the Guid values
            AddColumn("dbo.Membership", "NewMemberID", c => c.Guid(nullable: false));

            // Generate Guid values for existing records
            Sql("UPDATE dbo.Membership SET NewMemberID = NEWID()");

            // Drop the old MemberID column
            DropColumn("dbo.Membership", "MemberID");

            // Rename the NewMemberID column to MemberID
            RenameColumn("dbo.Membership", "NewMemberID", "MemberID");
        }

        public override void Down()
        {
            // This is your original Down method
            AlterColumn("dbo.Membership", "MemberID", c => c.Int(nullable: false));
        }
    }

}
