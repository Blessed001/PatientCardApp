using System;
using System.ComponentModel.DataAnnotations;

namespace PatientCardApp.Model
{
    public class PatientCard
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MidleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }


        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
    }
}
