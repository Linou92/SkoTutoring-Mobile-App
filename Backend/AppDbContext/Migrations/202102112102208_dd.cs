namespace AppDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SessionId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Session", t => t.SessionId)
                .Index(t => t.SessionId);
            
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        LevelId = c.Int(nullable: false),
                        TeacherId = c.String(),
                        StudentId = c.String(),
                        LangId = c.Int(nullable: false),
                        Purpose = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AvailabilityId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Notes = c.String(unicode: false, storeType: "text"),
                        Student_UserId = c.String(nullable: false, maxLength: 128),
                        Teacher_UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Availability", t => t.AvailabilityId)
                .ForeignKey("dbo.Student", t => t.Student_UserId)
                .ForeignKey("dbo.Teacher", t => t.Teacher_UserId)
                .ForeignKey("dbo.Language", t => t.LangId)
                .ForeignKey("dbo.Level", t => t.LevelId)
                .ForeignKey("dbo.Subject", t => t.SubjectId)
                .Index(t => t.SubjectId)
                .Index(t => t.LevelId)
                .Index(t => t.LangId)
                .Index(t => t.AvailabilityId)
                .Index(t => t.Student_UserId)
                .Index(t => t.Teacher_UserId);
            
            CreateTable(
                "dbo.Availability",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherAvailability",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Teacher_UserId = c.String(),
                        AvailabilityId = c.Int(nullable: false),
                        Teacher_UserId1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teacher", t => t.Teacher_UserId1)
                .ForeignKey("dbo.Availability", t => t.AvailabilityId)
                .Index(t => t.AvailabilityId)
                .Index(t => t.Teacher_UserId1);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId, unique: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        LevelId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.UserId, unique: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TeacherLang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Teacher_UserId = c.String(nullable: false, maxLength: 128),
                        LangId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LangId)
                .ForeignKey("dbo.Teacher", t => t.Teacher_UserId)
                .Index(t => t.Teacher_UserId)
                .Index(t => t.LangId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherLevel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Teacher_UserId = c.String(nullable: false, maxLength: 128),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Level", t => t.LevelId)
                .ForeignKey("dbo.Teacher", t => t.Teacher_UserId)
                .Index(t => t.Teacher_UserId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherSubj",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Teacher_UserId = c.String(nullable: false, maxLength: 128),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subject", t => t.SubjectId)
                .ForeignKey("dbo.Teacher", t => t.Teacher_UserId)
                .Index(t => t.Teacher_UserId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StudentLang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(),
                        LangId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentSubj",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(),
                        SubjId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TeacherAvailability", "AvailabilityId", "dbo.Availability");
            DropForeignKey("dbo.Teacher", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherSubj", "Teacher_UserId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherSubj", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.Session", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.TeacherLevel", "Teacher_UserId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherLevel", "LevelId", "dbo.Level");
            DropForeignKey("dbo.Session", "LevelId", "dbo.Level");
            DropForeignKey("dbo.TeacherLang", "Teacher_UserId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherLang", "LangId", "dbo.Language");
            DropForeignKey("dbo.Session", "LangId", "dbo.Language");
            DropForeignKey("dbo.TeacherAvailability", "Teacher_UserId1", "dbo.Teacher");
            DropForeignKey("dbo.Session", "Teacher_UserId", "dbo.Teacher");
            DropForeignKey("dbo.Teacher", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Student", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Student", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Session", "Student_UserId", "dbo.Student");
            DropForeignKey("dbo.Session", "AvailabilityId", "dbo.Availability");
            DropForeignKey("dbo.Assignment", "SessionId", "dbo.Session");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TeacherSubj", new[] { "SubjectId" });
            DropIndex("dbo.TeacherSubj", new[] { "Teacher_UserId" });
            DropIndex("dbo.TeacherLevel", new[] { "LevelId" });
            DropIndex("dbo.TeacherLevel", new[] { "Teacher_UserId" });
            DropIndex("dbo.TeacherLang", new[] { "LangId" });
            DropIndex("dbo.TeacherLang", new[] { "Teacher_UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Student", new[] { "CountryId" });
            DropIndex("dbo.Student", new[] { "UserId" });
            DropIndex("dbo.Teacher", new[] { "CountryId" });
            DropIndex("dbo.Teacher", new[] { "UserId" });
            DropIndex("dbo.TeacherAvailability", new[] { "Teacher_UserId1" });
            DropIndex("dbo.TeacherAvailability", new[] { "AvailabilityId" });
            DropIndex("dbo.Session", new[] { "Teacher_UserId" });
            DropIndex("dbo.Session", new[] { "Student_UserId" });
            DropIndex("dbo.Session", new[] { "AvailabilityId" });
            DropIndex("dbo.Session", new[] { "LangId" });
            DropIndex("dbo.Session", new[] { "LevelId" });
            DropIndex("dbo.Session", new[] { "SubjectId" });
            DropIndex("dbo.Assignment", new[] { "SessionId" });
            DropTable("dbo.StudentSubj");
            DropTable("dbo.StudentLang");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Subject");
            DropTable("dbo.TeacherSubj");
            DropTable("dbo.Level");
            DropTable("dbo.TeacherLevel");
            DropTable("dbo.Language");
            DropTable("dbo.TeacherLang");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Student");
            DropTable("dbo.Country");
            DropTable("dbo.Teacher");
            DropTable("dbo.TeacherAvailability");
            DropTable("dbo.Availability");
            DropTable("dbo.Session");
            DropTable("dbo.Assignment");
        }
    }
}
