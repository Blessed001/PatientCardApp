using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using PatientCardApp.UI.Data.Repositories;
using PatientCardApp.UI.Event;
using PatientCardApp.UI.View.Services;
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
        private  IMessageDialogService _messageDialogService;
        private  PatientCardWrapper _patientCard;
        private bool _hasChanges;


        public PatientCardDetailViewModel(IPatientCardRepository patientCardRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _patientCardRepository = patientCardRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute); 
            DeleteCommand = new DelegateCommand(OnDeleteExecute); 
        }

       
        public async Task LoadAsync(int? patienCardId)
        {
            var patientCard = patienCardId.HasValue
                ? await _patientCardRepository.GetByIdAsync(patienCardId.Value)
                : CreateNewPatientCard();

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
        public ICommand DeleteCommand { get; }

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
                    DisplayMember = $"{PatientCard.LastName}"
                });
        }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Вы уверено хотите удалить пациента {PatientCard.LastName}?",
                "Вопрос");
            if(result == MessageDialogResult.OK)
            {
                _patientCardRepository.Remove(PatientCard.Model);
                await _patientCardRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterPatientCardDeletedEvent>().Publish(PatientCard.Id);
            }
        }

        private PatientCard CreateNewPatientCard()
        {
            var patientCard = new PatientCard();
            _patientCardRepository.Add(patientCard);
            return patientCard;
        }

    }
}
