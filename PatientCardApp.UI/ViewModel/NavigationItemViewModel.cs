using PatientCardApp.UI.Event;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace PatientCardApp.UI.ViewModel
{
    public class NavigationItemViewModel:ViewModelBase
    {
        private string _detalViewModelName;
        private IEventAggregator _eventAggregator;
        private string _displayMember;

        public NavigationItemViewModel(int id, string displayMember,
            string detalViewModelName,
            IEventAggregator eventAggregator)
        {
             Id = id;
            DisplayMember = displayMember;
            _detalViewModelName = detalViewModelName;
            _eventAggregator = eventAggregator;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }

        public int Id { get; set; }
        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenDetailViewCommand { get; }
        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                   .Publish(
                new OpenDetailViewEventArgs
                {
                    Id=Id,
                    ViewModelName = _detalViewModelName
                }
                );
        }
    }
}
