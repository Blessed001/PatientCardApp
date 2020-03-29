using PatientCardApp.UI.Event;
using PatientCardApp.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientCardApp.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private Func<IPatientCardDetailViewModel> _patientCardDetailViewModelCreator;
        private IPatientCardDetailViewModel _patientCardDetailViewModel;


        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IPatientCardDetailViewModel> patientCardDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _patientCardDetailViewModelCreator = patientCardDetailViewModelCreator;

            _eventAggregator.GetEvent<OpenPatientCardDetailViewEvent>()
                .Subscribe(OnOpenPatientCardDetailView);
            _eventAggregator.GetEvent<AfterPatientCardDeletedEvent>()
                .Subscribe(AfterPatientCardDeleted);

            CreateNewPatientCardCommand = new DelegateCommand(OnCreateNewPatientCardExecute);

            NavigationViewModel = navigationViewModel;
        }

        
        public async Task LoadAsync()
        {
           await NavigationViewModel.LoadAsync();
        }

        public ICommand CreateNewPatientCardCommand { get; }

        public INavigationViewModel NavigationViewModel { get; }

        public IPatientCardDetailViewModel PatientCardDetailViewModel
        {
            get { return _patientCardDetailViewModel; }
            private set 
            { 
                _patientCardDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        private async void OnOpenPatientCardDetailView(int? patientCardId)
        {
            if(PatientCardDetailViewModel != null && PatientCardDetailViewModel.HasChanges)
            {
               var result = _messageDialogService.ShowOkCancelDialog("Вы будете терят данные", "Question");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            PatientCardDetailViewModel = _patientCardDetailViewModelCreator();
            await PatientCardDetailViewModel.LoadAsync(patientCardId);
        }
        private void OnCreateNewPatientCardExecute()
        {
            OnOpenPatientCardDetailView(null);
        }
        private void AfterPatientCardDeleted(int patientCardId)
        {
            PatientCardDetailViewModel = null;
        }

    }
}
