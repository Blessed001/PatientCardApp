
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
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            var lookup = await _patientCardLookUpDataService.GetPatientCardLookUpAsync();
            PatientCards.Clear();
            foreach (var item in lookup)
            {
                PatientCards.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                     nameof(PatientCardDetailViewModel),
                    _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItemViewModel> PatientCards { get; }
        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(PatientCardDetailViewModel):
                    var patientCard = PatientCards.SingleOrDefault(pc => pc.Id == args.Id);
                    if (patientCard != null)
                    {
                        PatientCards.Remove(patientCard);
                    }
                    break;
            }
        }
        private void AfterDetailSaved(AfterDetailSavedEventArgs obj)
        {
            switch(obj.ViewModelName)
            {
                case nameof(PatientCardDetailViewModel):
                var lookupItem = PatientCards.SingleOrDefault(pc => pc.Id == obj.Id);
                if (lookupItem == null)
                {
                    PatientCards.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember,
                        nameof(PatientCardDetailViewModel),
                        _eventAggregator));
                }
                else
                {
                    lookupItem.DisplayMember = obj.DisplayMember;
                }
                break;
            }
        }

    }
}
