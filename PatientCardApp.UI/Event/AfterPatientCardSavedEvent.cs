using Prism.Events;

namespace PatientCardApp.UI.Event
{
    public class AfterPatientCardSavedEvent:PubSubEvent<AfterPatientCardSavedEventArgs>
    {
    }

    public class AfterPatientCardSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
