using PatientCardApp.DataAccess;
using PatientCardApp.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PatientCardApp.UI.Data.Repositories
{
    public class PatientCardRepository : GenericRepository<PatientCard, PatientCardContext>,
                                         IPatientCardRepository
    {
        public PatientCardRepository(PatientCardContext context):base(context)
        {
        }

        public override async Task<PatientCard> GetByIdAsync( int patientCardId)
        {
            return await _context.PatientCards
                .Include(pc => pc.Visits)
                .SingleAsync(pc => pc.Id == patientCardId);
        }
        public void RemoveVisit(Visit model)
        {
            _context.Visits.Remove(model);
        }
    }
}
