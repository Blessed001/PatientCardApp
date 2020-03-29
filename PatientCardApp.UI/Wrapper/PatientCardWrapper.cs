using PatientCardApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PatientCardApp.UI.Wrapper
{
    public class PatientCardWrapper:ModelWrapper<PatientCard>
    {
        public PatientCardWrapper(PatientCard model) : base(model)
        {
        }

        public int Id { get { return Model.Id; }}

        public string FirstName
        {
            get { return GetValue<string>();}
            set {SetValue(value);}
        }

        public string MidleName
        {
            get { return GetValue<string>();}
            set{SetValue(value);}
        }

        public string LastName
        {
            get { return GetValue<string>();}
            set{SetValue(value);}
        }

        public string Gender
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public DateTime BirthDay
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public string Address
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string PhoneNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        //protected override IEnumerable<string> ValidateProperty(string propertyName)
        //{
        //    switch (propertyName)
        //    {
        //        case nameof(FirstName):
        //            if (string.Equals(FirstName, "Robots", StringComparison.OrdinalIgnoreCase))
        //            {
        //                yield return "Robots are not valid name";
        //            }
        //            break;
        //    }
        //}
    }
}
