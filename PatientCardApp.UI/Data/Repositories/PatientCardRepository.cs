using PatientCardApp.DataAccess;
using PatientCardApp.Model;
using System.Data.Entity;
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

        public void Add(PatientCard patientCard)
        {
            _context.PatientCards.Add(patientCard);
        }

        public async Task<PatientCard> GetByIdAsync( int patientCardId)
        {
            return await _context.PatientCards
                .Include(pc => pc.Visits)
                .SingleAsync(pc => pc.Id == patientCardId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(PatientCard model)
        {
            _context.PatientCards.Remove(model);
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
