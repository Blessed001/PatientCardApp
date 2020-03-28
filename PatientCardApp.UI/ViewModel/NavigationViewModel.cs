using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using PatientCardApp.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public class NavigationViewModel :ViewModelBase, INavigationViewModel
    {
        private  IPatientCardLookUpDataService _patientCardLookUpDataService;
        private  IEventAggregator _eventAggregator;

        public NavigationViewModel(IPatientCardLookUpDataService patientCardLookUpDataService,
            IEventAggregator eventAggregator)
        {
            _patientCardLookUpDataService = patientCardLookUpDataService;
            _eventAggregator = eventAggregator;
            PatientCards = new ObservableCollection<LookUpItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _patientCardLookUpDataService.GetPatientCardLookUpAsync();
            PatientCards.Clear();
            foreach (var item in lookup)
            {
                PatientCards.Add(item);
            }
        }

        public ObservableCollection<LookUpItem> PatientCards { get; }

        private LookUpItem _selectedPatientCard;

        public LookUpItem SelectedPatientCard
        {
            get { return _selectedPatientCard; }
            set { _selectedPatientCard = value;
                OnPropertyChanged();
                if(_selectedPatientCard != null)
                {
                    _eventAggregator.GetEvent<OpenPatientCardDetailViewEvent>()
                    .Publish(_selectedPatientCard.Id);
                }
            }
        }


    }
}
