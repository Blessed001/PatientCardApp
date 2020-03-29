using PatientCardApp.UI.Event;
using PatientCardApp.UI.View.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

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

            NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
           await NavigationViewModel.LoadAsync();
        }

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

        private async void OnOpenPatientCardDetailView(int patientCardId)
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

    }
}
