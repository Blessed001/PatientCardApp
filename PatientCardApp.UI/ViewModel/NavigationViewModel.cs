
using PatientCardApp.UI.Data.Lookups;
using PatientCardApp.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
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
            PatientCards = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterPatientCardSavedEvent>().Subscribe(AfterPatientCardSaved);
        }

        private void AfterPatientCardSaved(AfterPatientCardSavedEventArgs obj)
        {
            var lookupItem = PatientCards.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _patientCardLookUpDataService.GetPatientCardLookUpAsync();
            PatientCards.Clear();
            foreach (var item in lookup)
            {
                PatientCards.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItemViewModel> PatientCards { get; }
    }
}
