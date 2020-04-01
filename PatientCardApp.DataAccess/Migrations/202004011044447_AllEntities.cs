namespace PatientCardApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        MidleName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        BirthDay = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        GenderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genders", t => t.GenderId)
                .Index(t => t.GenderId);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfVisit = c.DateTime(nullable: false),
                        Diagnosis = c.String(maxLength: 1500),
                        PatientCardId = c.Int(nullable: false),
                        TypeOfVisitId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientCards", t => t.PatientCardId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfVisits", t => t.TypeOfVisitId)
                .Index(t => t.PatientCardId)
                .Index(t => t.TypeOfVisitId);
            
            CreateTable(
                "dbo.TypeOfVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "TypeOfVisitId", "dbo.TypeOfVisits");
            DropForeignKey("dbo.Visits", "PatientCardId", "dbo.PatientCards");
            DropForeignKey("dbo.PatientCards", "GenderId", "dbo.Genders");
            DropIndex("dbo.Visits", new[] { "TypeOfVisitId" });
            DropIndex("dbo.Visits", new[] { "PatientCardId" });
            DropIndex("dbo.PatientCards", new[] { "GenderId" });
            DropTable("dbo.TypeOfVisits");
            DropTable("dbo.Visits");
            DropTable("dbo.PatientCards");
            DropTable("dbo.Genders");
        }
    }
}
