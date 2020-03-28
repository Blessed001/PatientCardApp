using PatientCardApp.DataAccess;
using PatientCardApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PatientCardApp.UI.Data
{
    public class PatientCardDataServices : IPatientCardDataServices
    {
        private readonly Func<PatientCardContext> _contextCreator;

        public PatientCardDataServices(Func<PatientCardContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<List<PatientCard>> GetAllAsync()
        {
            using(var ctx = _contextCreator())
            {
                return await ctx.PatientCards.AsNoTracking().ToListAsync();
            }
        }
    }
}
