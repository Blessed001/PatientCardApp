using PatientCardApp.DataAccess;
using PatientCardApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PatientCardApp.UI.Data.Repositories
{
    public class PatientCardRepository : IPatientCardRepository
    {
        private readonly PatientCardContext _context;

        public PatientCardRepository(PatientCardContext context)
        {
            _context = context ;
        }
        public async Task<PatientCard> GetByIdAsync( int patientCardId)
        {
            return await _context.PatientCards.SingleAsync(pc => pc.Id == patientCardId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
