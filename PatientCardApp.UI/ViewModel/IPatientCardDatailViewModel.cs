using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public interface IPatientCardDatailViewModel
    {
        Task LoadAsync(int patienCardId);
    }
}