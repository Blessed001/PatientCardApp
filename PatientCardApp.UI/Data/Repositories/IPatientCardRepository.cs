using PatientCardApp.Model;

namespace PatientCardApp.UI.Data.Repositories
{
    public interface IPatientCardRepository : IGenericRepository<PatientCard>
    {
        void RemoveVisit(Visit model);
    }
}