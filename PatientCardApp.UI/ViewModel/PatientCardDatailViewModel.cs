using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCardApp.UI.ViewModel
{
    public class PatientCardDatailViewModel : ViewModelBase, IPatientCardDatailViewModel
    {
        private IPatientCardDataService _patientCardDataService;

        public PatientCardDatailViewModel(IPatientCardDataService patientCardDataService)
        {
            _patientCardDataService = patientCardDataService;
        }

        public async Task LoadAsync(int patienCardId)
        {
            PatientCard = await _patientCardDataService.GetByIdAsync(patienCardId);
        }

        private PatientCard _patientCard;

        public PatientCard PatientCard
        {
            get { return _patientCard; }
            private set
            {
                _patientCard = value;
                OnPropertyChanged();
            }
        }
    }
}
