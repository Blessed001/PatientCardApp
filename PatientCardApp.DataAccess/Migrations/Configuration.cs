namespace PatientCardApp.DataAccess.Migrations
{
    using PatientCardApp.Model;
    using System;
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
            context.TypeOfVisits.AddOrUpdate(
                t => t.Name,
                  new TypeOfVisit { Name = "Первичный" },
                  new TypeOfVisit { Name = "Вторичный" }
                );

            context.Genders.AddOrUpdate(
                t => t.Name,
                  new Gender { Name = "Мужской" },
                  new Gender { Name = "Женской" }
                );
            context.SaveChanges();

            context.PatientCards.AddOrUpdate(
                f => f.FirstName,
                new PatientCard { FirstName = "Patient 1", MidleName = "Midle 1", LastName = "Patient Last 1", Address = "Address 1", BirthDay = DateTime.Parse("2020-01-12"), PhoneNumber = "8900234567", GenderId = 1 },
                new PatientCard { FirstName = "Patient 2", MidleName = "Midle 2", LastName = "Patient Last 2", Address = "Address 2", BirthDay = DateTime.Parse("2020-01-12"), PhoneNumber = "8900234567", GenderId = 2 },
                new PatientCard { FirstName = "Patient 3", MidleName = "Midle 3", LastName = "Patient Last 3", Address = "Address 3", BirthDay = DateTime.Parse("2020-01-12"), PhoneNumber = "8900234567", GenderId = 2 },
                new PatientCard { FirstName = "Patient 4", MidleName = "Midle 4", LastName = "Patient Last 4", Address = "Address 4", BirthDay = DateTime.Parse("2020-01-12"), PhoneNumber = "8900234567", GenderId = 1 }
                );
            context.SaveChanges();

            context.Visits.AddOrUpdate(
                d => d.DayOfVisit,
                  new Visit { DayOfVisit = DateTime.Parse("2020-01-12"), Diagnosis = "Diagnosis text", PatientCardId = context.PatientCards.First().Id, TypeOfVisitId = context.TypeOfVisits.First().Id }
                );
        }
    }
}