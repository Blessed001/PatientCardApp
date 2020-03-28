using PatientCardApp.DataAccess;
using PatientCardApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientCardApp.UI.Data
{
    public class PatientCardDataServices : IPatientCardDataServices
    {
        private readonly Func<PatientCardContext> _contextCreator;

        public PatientCardDataServices(Func<PatientCardContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public IEnumerable<PatientCard> GetAll()
        {
           using(var ctx = _contextCreator())
            {
                return ctx.PatientCards.AsNoTracking().ToList();
            }
        }
    }
}
