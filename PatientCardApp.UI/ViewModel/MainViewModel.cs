using PatientCardApp.Model;
using PatientCardApp.UI.Data;
using System.Collections.ObjectModel;


namespace PatientCardApp.UI.ViewModel
{
    class MainViewModel
    {
        private readonly IPatientCardDataServices _patientCardDataServices;
        private PatientCard _selectedPatient;


        public MainViewModel(IPatientCardDataServices patientCardDataServices)
        {
            PatientCards = new ObservableCollection<PatientCard>();
            _patientCardDataServices = patientCardDataServices;
        }

        public void Load()
        {
            var patientCards = _patientCardDataServices.GetAll();
            PatientCards.Clear();

            foreach(var patientCard in patientCards)
            {
                PatientCards.Add(patientCard);
            }
        }
        public ObservableCollection<PatientCard> PatientCards  { get; set; }

        public PatientCard SelectedPatient
        {
            get { return _selectedPatient; }
            set { _selectedPatient = value; }
        }

    }
}
