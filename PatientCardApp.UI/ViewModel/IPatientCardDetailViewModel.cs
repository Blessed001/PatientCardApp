using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public interface IPatientCardDetailViewModel
    {
        Task LoadAsync(int patienCardId);
        bool HasChanges { get; }
    }
}