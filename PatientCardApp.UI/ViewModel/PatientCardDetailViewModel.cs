using PatientCardApp.UI.Data;
using PatientCardApp.UI.Event;
using PatientCardApp.UI.Wrapper;
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
        private  PatientCardWrapper _patientCard;

        public PatientCardDetailViewModel(IPatientCardDataService patientCardDataService,
            IEventAggregator eventAggregator)
        {
            _patientCardDataService = patientCardDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenPatientCardDetailViewEvent>()
                .Subscribe(OnOpenPatientCardDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }
        public async Task LoadAsync(int patienCardId)
        {
            var patientCard = await _patientCardDataService.GetByIdAsync(patienCardId);

            PatientCard = new PatientCardWrapper(patientCard);
            PatientCard.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(PatientCard.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public PatientCardWrapper PatientCard
        {
            get { return _patientCard; }
            private set
            {
                _patientCard = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; }

        private bool OnSaveCanExecute()
        {
            return PatientCard != null && !PatientCard.HasErrors;
        }

        private async void OnSaveExecute()
        {
          await _patientCardDataService.SaveAsync(PatientCard.Model);
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
    }
}
