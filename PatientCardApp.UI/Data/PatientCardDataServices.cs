using PatientCardApp.Model;
using System.Collections.Generic;


namespace PatientCardApp.UI.Data
{
    public class PatientCardDataServices : IPatientCardDataServices
    {
        public IEnumerable<PatientCard> GetAll()
        {
            yield return new PatientCard { FirstName = "Patient 1", LastName = "Patient Last 1" };
            yield return new PatientCard { FirstName = "Patient 2", LastName = "Patient Last 2" };
            yield return new PatientCard { FirstName = "Patient 3", LastName = "Patient Last 3" };
            yield return new PatientCard { FirstName = "Patient 4", LastName = "Patient Last 4" };
        }
    }
}
