using System.Collections.Generic;
using System.Threading.Tasks;
using PatientCardApp.Model;

namespace PatientCardApp.UI.Data.Lookups
{
    public interface IPatientCardLookUpDataService
    {
        Task<IEnumerable<LookUpItem>> GetPatientCardLookUpAsync();
    }
}