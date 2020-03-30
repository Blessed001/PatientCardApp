using PatientCardApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCardApp.UI.Wrapper
{
    public class VisitWrapper: ModelWrapper<Visit>
    {
        public VisitWrapper(Visit model):base(model)
        {

        }
        public DateTime DayOfVisit
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public string Diagnosis
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
