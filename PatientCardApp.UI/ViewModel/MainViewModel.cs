using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel(INavigationViewModel navigationViewModel,
            IPatientCardDatailViewModel patientCardDatailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            PatientCardDatailViewModel = patientCardDatailViewModel;
        }

        public async Task LoadAsync()
        {
           await NavigationViewModel.LoadAsync();
        }

        public INavigationViewModel NavigationViewModel { get; }
        public IPatientCardDatailViewModel PatientCardDatailViewModel { get; }
        
    }
}
