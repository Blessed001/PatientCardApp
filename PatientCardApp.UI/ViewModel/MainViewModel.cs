using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private readonly IPatientCardDataServices _patientCardDataServices;
        private PatientCard _selectedPatient;


        public MainViewModel(IPatientCardDataServices patientCardDataServices)
        {
            PatientCards = new ObservableCollection<PatientCard>();
            _patientCardDataServices = patientCardDataServices;
        }

        public async Task LoadAsync()
        {
            var patientCards = await _patientCardDataServices.GetAllAsync();
            PatientCards.Clear();

            foreach(var patientCard in patientCards)
            {
                PatientCards.Add(patientCard);
            }
        }
        public ObservableCollection<PatientCard> PatientCards  { get; set; }

        public PatientCard SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }
    }
}
