using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public class NavigationViewModel : INavigationViewModel
    {
        private readonly IPatientCardLookUpDataService _patientCardLookUpDataService;

        public ObservableCollection<LookUpItem> PatientCards { get; }

        public NavigationViewModel(IPatientCardLookUpDataService patientCardLookUpDataService)
        {
            _patientCardLookUpDataService = patientCardLookUpDataService;
            PatientCards = new ObservableCollection<LookUpItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _patientCardLookUpDataService.GetPatientCardLookUpAsync();
            PatientCards.Clear();
            foreach (var item in lookup)
            {
                PatientCards.Add(item);
            }
        }
    }
}
