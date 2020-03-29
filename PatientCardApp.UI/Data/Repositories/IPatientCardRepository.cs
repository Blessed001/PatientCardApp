using System.Collections.Generic;
using System.Threading.Tasks;
using PatientCardApp.Model;

namespace PatientCardApp.UI.Data.Repositories
{
    public interface IPatientCardRepository
    {
        Task<PatientCard> GetByIdAsync(int patientCardId);
        Task SaveAsync();
        bool HasChanges();
        void Add(PatientCard patientCard);
        void Remove(PatientCard model);
    }
}