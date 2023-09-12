namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class changedstaffidtoguid : DbMigration
    {
        public override void Up()
        {
            // Add a new Guid column
            AddColumn("dbo.TrainingSessions", "NewStaffID", c => c.Guid(nullable: false));

            // Generate Guid values for existing records
            Sql("UPDATE dbo.TrainingSessions SET NewStaffID = NEWID()");

            // Drop the old StaffID column
            DropColumn("dbo.TrainingSessions", "StaffID");

            // Rename the NewStaffID column to StaffID
            RenameColumn("dbo.TrainingSessions", "NewStaffID", "StaffID");
        }

        public override void Down()
        {
            // This is your original Down method
            AlterColumn("dbo.TrainingSessions", "StaffID", c => c.Int(nullable: false));
        }
    }
}
