namespace PatientCardApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientCards", "Address", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientCards", "Address", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
