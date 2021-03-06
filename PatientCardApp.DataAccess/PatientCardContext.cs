﻿using PatientCardApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCardApp.DataAccess
{
    public class PatientCardContext:DbContext
    {
        public PatientCardContext():base("PatientCardConnection")
        {

        }
        public DbSet<PatientCard> PatientCards { get; set; }
        public DbSet<TypeOfVisit> TypeOfVisits { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}
