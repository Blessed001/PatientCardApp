namespace PatientCardApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Visits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfVisit = c.DateTime(nullable: false),
                        Diagnosis = c.String(maxLength: 1500),
                        PatientCardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientCard", t => t.PatientCardId, cascadeDelete: true)
                .Index(t => t.PatientCardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visit", "PatientCardId", "dbo.PatientCard");
            DropIndex("dbo.Visit", new[] { "PatientCardId" });
            DropTable("dbo.Visit");
        }
    }
}
