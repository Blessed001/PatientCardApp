using System.Collections.Generic;
using PatientCardApp.Model;

namespace PatientCardApp.UI.Data
{
    public interface IPatientCardDataServices
    {
        IEnumerable<PatientCard> GetAll();
    }
}