using PatientCardApp.UI.Data;
using PatientCardApp.UI.Data.Repositories;
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
        private  IPatientCardRepository _patientCardRepository;
        private  IEventAggregator _eventAggregator;
        private  PatientCardWrapper _patientCard;
        private bool _hasChanges;


        public PatientCardDetailViewModel(IPatientCardRepository patientCardRepository,
            IEventAggregator eventAggregator)
        {
            _patientCardRepository = patientCardRepository;
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }
        public async Task LoadAsync(int patienCardId)
        {
            var patientCard = await _patientCardRepository.GetByIdAsync(patienCardId);

            PatientCard = new PatientCardWrapper(patientCard);
            PatientCard.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _patientCardRepository.HasChanges();
                }
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

        public bool HasChanges
        {
            get { return _hasChanges; }
            set 
            {
                if(_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }


        public ICommand SaveCommand { get; }

        private bool OnSaveCanExecute()
        {
            return PatientCard != null && !PatientCard.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
          await _patientCardRepository.SaveAsync();
            HasChanges = _patientCardRepository.HasChanges();          
            _eventAggregator.GetEvent<AfterPatientCardSavedEvent>().Publish(
                new AfterPatientCardSavedEventArgs
                {
                    Id = PatientCard.Id,
                    DisplayMember = $"{PatientCard.LastName} {PatientCard.FirstName} {PatientCard.MidleName}"
                });
        }
    }
}
