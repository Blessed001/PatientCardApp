﻿using Prism.Events;

namespace PatientCardApp.UI.Event
{
    public class AfterDetailDeletedEvent:PubSubEvent<AfterDetailDeletedEventArgs>
    {
    }

    public class AfterDetailDeletedEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
