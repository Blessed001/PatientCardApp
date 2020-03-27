namespace PatientCardApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientCard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MidleName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientCard");
        }
    }
}
