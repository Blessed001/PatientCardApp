namespace PatientCardApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfVisit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientCard", "TypeOfVisitId", "dbo.TypeOfVisit");
            DropIndex("dbo.PatientCard", new[] { "TypeOfVisitId" });
            AddColumn("dbo.Visit", "TypeOfVisitId", c => c.Int());
            CreateIndex("dbo.Visit", "TypeOfVisitId");
            AddForeignKey("dbo.Visit", "TypeOfVisitId", "dbo.TypeOfVisit", "Id");
            DropColumn("dbo.PatientCard", "TypeOfVisitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientCard", "TypeOfVisitId", c => c.Int());
            DropForeignKey("dbo.Visit", "TypeOfVisitId", "dbo.TypeOfVisit");
            DropIndex("dbo.Visit", new[] { "TypeOfVisitId" });
            DropColumn("dbo.Visit", "TypeOfVisitId");
            CreateIndex("dbo.PatientCard", "TypeOfVisitId");
            AddForeignKey("dbo.PatientCard", "TypeOfVisitId", "dbo.TypeOfVisit", "Id");
        }
    }
}
