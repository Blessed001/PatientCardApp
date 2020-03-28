using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using PatientCardApp.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

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

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }

        private async void OnSaveExecute()
        {
          await _patientCardDataService.SaveAsync(PatientCard);
            _eventAggregator.GetEvent<AfterPatientCardSavedEvent>().Publish(
                new AfterPatientCardSavedEventArgs
                {
                    Id = PatientCard.Id,
                    DisplayMember = $"{PatientCard.LastName} {PatientCard.FirstName} {PatientCard.MidleName}"
                });
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
        public ICommand SaveCommand { get; }
    }
}
