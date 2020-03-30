using System.Collections.Generic;
using System.Threading.Tasks;
using PatientCardApp.Model;

namespace PatientCardApp.UI.Data.Lookups
{
    public interface ITypeOfVisitLookUpDataService
    {
        Task<IEnumerable<LookUpItem>> GetTypeOfVisitLookUpAsync();
    }
}