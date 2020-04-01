using PatientCardApp.Model;
using PatientCardApp.UI.Data.Lookups;
using PatientCardApp.UI.Data.Repositories;
using PatientCardApp.UI.View.Services;
using PatientCardApp.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
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
        private readonly IPatientCardRepository _patientCardRepository;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IGenderLookUpDataService _genderLookUpDataService;
        private  PatientCardWrapper _patientCard;
        private VisitWrapper _selectedVisit;


        public PatientCardDetailViewModel(IPatientCardRepository patientCardRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IGenderLookUpDataService genderLookUpDataService):base(eventAggregator)
        {
            _patientCardRepository = patientCardRepository;
            _messageDialogService = messageDialogService;
            _genderLookUpDataService = genderLookUpDataService;
            AddVisitCommand = new DelegateCommand(OnAddVisitExecute);
            RemoveVisitCommand = new DelegateCommand(OnRemoveVisitExecute, OnRemoveCanExecute);

            Genders = new ObservableCollection<LookUpItem>();

            Visits = new ObservableCollection<VisitWrapper>();


        }

        public override async Task LoadAsync(int? patienCardId)
        {
            var patientCard = patienCardId.HasValue
                ? await _patientCardRepository.GetByIdAsync(patienCardId.Value)
                : CreateNewPatientCard();

            InitializePatientCard(patientCard);
            InitializeVisits(patientCard.Visits);
            await LoadGendersLookupAsync();
        }

        private async Task LoadGendersLookupAsync()
        {
            Genders.Clear();
            Genders.Add(new NullLookupItem { DisplayMember = "Выбрать пол" });
            var lookup = await _genderLookUpDataService.GetGenderLookUpAsync();
            foreach (var lookupItem in lookup)
            {
                Genders.Add(lookupItem);
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
        public ObservableCollection<LookUpItem> Genders { get;}
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
