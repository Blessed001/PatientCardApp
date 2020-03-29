using PatientCardApp.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientCardApp.UI.ViewModel
{
    public class NavigationItemViewModel:ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private string _displayMember;

        public NavigationItemViewModel(int id, string displayMember,
            IEventAggregator eventAggregator)
        {
             Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;
            OpenPatientCardDetailViewCommand = new DelegateCommand(OnOpenPatientCardDetailView);
        }

        public int Id { get; set; }
        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenPatientCardDetailViewCommand { get; }
        private void OnOpenPatientCardDetailView()
        {
            _eventAggregator.GetEvent<OpenPatientCardDetailViewEvent>()
                   .Publish(Id);
        }
    }
}
