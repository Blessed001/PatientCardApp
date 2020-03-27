namespace PatientCardApp.DataAccess.Migrations
{
    using PatientCardApp.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PatientCardApp.DataAccess.PatientCardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PatientCardApp.DataAccess.PatientCardContext context)
        {
            context.PatientCards.AddOrUpdate(
                f => f.FirstName,
                new PatientCard { FirstName = "Patient 1", LastName = "Patient Last 1" },
                new PatientCard { FirstName = "Patient 2", LastName = "Patient Last 2" },
                new PatientCard { FirstName = "Patient 3", LastName = "Patient Last 3" },
                new PatientCard { FirstName = "Patient 4", LastName = "Patient Last 4" }
                );
        }
    }
}
