using System.Collections.Generic;
using System.Threading.Tasks;
using PatientCardApp.Model;

namespace PatientCardApp.UI.Data.Lookups
{
    public interface IGenderLookUpDataService
    {
        Task<IEnumerable<LookUpItem>> GetGenderLookUpAsync();
    }
}