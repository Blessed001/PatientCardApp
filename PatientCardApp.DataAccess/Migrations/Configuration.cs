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
                new PatientCard { FirstName = "Юрий", MidleName = "Васильевич", LastName = "Шпаков", Address = "Улица Калинина 13 к 55  ", BirthDay = DateTime.Parse("1988-01-15"), PhoneNumber = "8900234567", GenderId = 1 },
                new PatientCard { FirstName = "Светлана", MidleName = "Владимировна", LastName = "Лобусь", Address = "Улица Воровского 165", BirthDay = DateTime.Parse("1965-03-22"), PhoneNumber = "8900234567", GenderId = 2 },
                new PatientCard { FirstName = "Вероника", MidleName = "Юрьевна", LastName = "Знаменская", Address = "Улица Мира д45", BirthDay = DateTime.Parse("1991-09-15"), PhoneNumber = "8900234567", GenderId = 2 },
                new PatientCard { FirstName = "Александр", MidleName = "Михайлович", LastName = "Смирнов", Address = "Улица Ленина 188", BirthDay = DateTime.Parse("1986-08-08"), PhoneNumber = "8900234567", GenderId = 1 }
                );
            context.SaveChanges();

            context.Visits.AddOrUpdate(
                d => d.DayOfVisit,
                  new Visit { DayOfVisit = DateTime.Parse("2020-01-12"), Diagnosis = "Lorem Ipsum - это текст-рыба, часто используемый в печати и вэб-дизайне.", PatientCardId = context.PatientCards.First().Id, TypeOfVisitId = context.TypeOfVisits.First().Id }
                );
        }
    }
}