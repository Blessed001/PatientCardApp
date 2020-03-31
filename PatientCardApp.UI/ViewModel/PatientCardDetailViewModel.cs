using PatientCardApp.Model;
using PatientCardApp.UI.Data.Lookups;
using PatientCardApp.UI.Data.Repositories;
using PatientCardApp.UI.Event;
using PatientCardApp.UI.View.Services;
using PatientCardApp.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientCardApp.UI.ViewModel
{
    public class PatientCardDetailViewModel : DetailViewModelBase, IPatientCardDetailViewModel
    {
        private  IPatientCardRepository _patientCardRepository;
        private  IMessageDialogService _messageDialogService;
        private  ITypeOfVisitLookUpDataService _typeOfVisitLookUpDataService;
        private  PatientCardWrapper _patientCard;
        private VisitWrapper _selectedVisit;


        public PatientCardDetailViewModel(IPatientCardRepository patientCardRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            ITypeOfVisitLookUpDataService typeOfVisitLookUpDataService):base(eventAggregator)
        {
            _patientCardRepository = patientCardRepository;
            _messageDialogService = messageDialogService;
            _typeOfVisitLookUpDataService = typeOfVisitLookUpDataService;
            AddVisitCommand = new DelegateCommand(OnAddVisitExecute);
            RemoveVisitCommand = new DelegateCommand(OnRemoveVisitExecute, OnRemoveCanExecute);

            TypeOfVisits = new ObservableCollection<LookUpItem>();

            Visits = new ObservableCollection<VisitWrapper>();


        }

        public override async Task LoadAsync(int? patienCardId)
        {
            var patientCard = patienCardId.HasValue
                ? await _patientCardRepository.GetByIdAsync(patienCardId.Value)
                : CreateNewPatientCard();

            InitializePatientCard(patientCard);
            InitializeVisits(patientCard.Visits);
            await LoadTypeOfVisitsLookupAsync();
        }

        private async Task LoadTypeOfVisitsLookupAsync()
        {
            TypeOfVisits.Clear();
            TypeOfVisits.Add(new NullLookupItem { DisplayMember = "===="});
            var lookup = await _typeOfVisitLookUpDataService.GetTypeOfVisitLookUpAsync();
            foreach (var lookupItem in lookup)
            {
                TypeOfVisits.Add(lookupItem);
            }
        }

        private void InitializePatientCard(PatientCard patientCard)
        {
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

        private void InitializeVisits(ICollection<Visit> visits)
        {
            foreach(var wrapper in Visits)
            {
                wrapper.PropertyChanged -= VisitWrapper_PropertyChanged;
            }
            Visits.Clear();
            foreach(var visit in visits)
            {
                var wrapper = new VisitWrapper(visit);
                Visits.Add(wrapper);
                wrapper.PropertyChanged += VisitWrapper_PropertyChanged;

            }
        }

        private void VisitWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _patientCardRepository.HasChanges();
            }
            if (e.PropertyName == nameof(VisitWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
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

        public VisitWrapper SelectedVisit
        {
            get { return _selectedVisit; }
            set
            {
                _selectedVisit = value;
                    OnPropertyChanged();
                    ((DelegateCommand)RemoveVisitCommand).RaiseCanExecuteChanged();
            }
        }
        public ICommand AddVisitCommand { get; }
        public ICommand RemoveVisitCommand { get; }
        public ObservableCollection<LookUpItem> TypeOfVisits { get; }
        public ObservableCollection<VisitWrapper> Visits { get; }

        protected override bool OnSaveCanExecute()
        {
            return PatientCard != null 
                && !PatientCard.HasErrors 
                && Visits.All(v => !v.HasErrors)
                && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
           await _patientCardRepository.SaveAsync();
            HasChanges = _patientCardRepository.HasChanges();
            RaiseDetailSavedEvent(PatientCard.Id, $"{PatientCard.LastName}");
        }
        protected override async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Вы уверено хотите удалить пациента {PatientCard.LastName}?",
                "Вопрос");
            if(result == MessageDialogResult.OK)
            {
                _patientCardRepository.Remove(PatientCard.Model);
                await _patientCardRepository.SaveAsync();
                RaiseDetailDeletedEvent(PatientCard.Id);
            }
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedVisit != null;
        }

        private void OnRemoveVisitExecute()
        {
            SelectedVisit.PropertyChanged -= VisitWrapper_PropertyChanged;
            _patientCardRepository.RemoveVisit(SelectedVisit.Model);
            Visits.Remove(SelectedVisit);
            SelectedVisit = null;
            HasChanges = _patientCardRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddVisitExecute()
        {
            var newVisit = new VisitWrapper(new Visit());
            newVisit.PropertyChanged += VisitWrapper_PropertyChanged;
            Visits.Add(newVisit);
            PatientCard.Model.Visits.Add(newVisit.Model);
        }

        private PatientCard CreateNewPatientCard()
        {
            var patientCard = new PatientCard();
            _patientCardRepository.Add(patientCard);
            return patientCard;
        }

    }
}
