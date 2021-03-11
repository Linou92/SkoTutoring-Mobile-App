namespace AppDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Session", new[] { "Student_UserId" });
            DropIndex("dbo.Session", new[] { "Teacher_UserId" });
            DropColumn("dbo.Session", "StudentId");
            DropColumn("dbo.Session", "TeacherId");
            RenameColumn(table: "dbo.Session", name: "Student_UserId", newName: "StudentId");
            RenameColumn(table: "dbo.Session", name: "Teacher_UserId", newName: "TeacherId");
            AlterColumn("dbo.Session", "TeacherId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Session", "StudentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Session", "TeacherId");
            CreateIndex("dbo.Session", "StudentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Session", new[] { "StudentId" });
            DropIndex("dbo.Session", new[] { "TeacherId" });
            AlterColumn("dbo.Session", "StudentId", c => c.String());
            AlterColumn("dbo.Session", "TeacherId", c => c.String());
            RenameColumn(table: "dbo.Session", name: "TeacherId", newName: "Teacher_UserId");
            RenameColumn(table: "dbo.Session", name: "StudentId", newName: "Student_UserId");
            AddColumn("dbo.Session", "TeacherId", c => c.String());
            AddColumn("dbo.Session", "StudentId", c => c.String());
            CreateIndex("dbo.Session", "Teacher_UserId");
            CreateIndex("dbo.Session", "Student_UserId");
        }
    }
}
