using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel(INavigationViewModel navigationViewModel,
            IPatientCardDetailViewModel patientCardDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            PatientCardDetailViewModel = patientCardDetailViewModel;
        }

        public async Task LoadAsync()
        {
           await NavigationViewModel.LoadAsync();
        }

        public INavigationViewModel NavigationViewModel { get; }
        public IPatientCardDetailViewModel PatientCardDetailViewModel { get; }
        
    }
}
