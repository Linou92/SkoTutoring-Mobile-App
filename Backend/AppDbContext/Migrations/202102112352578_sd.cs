namespace AppDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TeacherAvailability", new[] { "Teacher_UserId1" });
            DropColumn("dbo.TeacherAvailability", "Teacher_UserId");
            RenameColumn(table: "dbo.TeacherAvailability", name: "Teacher_UserId1", newName: "Teacher_UserId");
            AlterColumn("dbo.TeacherAvailability", "Teacher_UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TeacherAvailability", "Teacher_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TeacherAvailability", new[] { "Teacher_UserId" });
            AlterColumn("dbo.TeacherAvailability", "Teacher_UserId", c => c.String());
            RenameColumn(table: "dbo.TeacherAvailability", name: "Teacher_UserId", newName: "Teacher_UserId1");
            AddColumn("dbo.TeacherAvailability", "Teacher_UserId", c => c.String());
            CreateIndex("dbo.TeacherAvailability", "Teacher_UserId1");
        }
    }
}
