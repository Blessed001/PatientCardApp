using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using PatientCardApp.UI.Event;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public class PatientCardDetailViewModel : ViewModelBase, IPatientCardDetailViewModel
    {
        private  IPatientCardDataService _patientCardDataService;
        private  IEventAggregator _eventAggregator;

        public PatientCardDetailViewModel(IPatientCardDataService patientCardDataService,
            IEventAggregator eventAggregator)
        {
            _patientCardDataService = patientCardDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenPatientCardDetailViewEvent>()
                .Subscribe(OnOpenPatientCardDetailView);
        }

        private async void OnOpenPatientCardDetailView(int patientCardId)
        {
            await LoadAsync(patientCardId);
        }

        public async Task LoadAsync(int patienCardId)
        {
            PatientCard = await _patientCardDataService.GetByIdAsync(patienCardId);
        }

        private PatientCard _patientCard;

        public PatientCard PatientCard
        {
            get { return _patientCard; }
            private set
            {
                _patientCard = value;
                OnPropertyChanged();
            }
        }
    }
}
