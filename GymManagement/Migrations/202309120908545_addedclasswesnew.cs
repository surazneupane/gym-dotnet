namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedclasswesnew : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TrainingSession", newName: "TrainingSessions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TrainingSessions", newName: "TrainingSession");
        }
    }
}
