using System;
using System.ComponentModel.DataAnnotations;

namespace PatientCardApp.Model
{
    public class PatientCard
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(100, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(100, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        public string MidleName { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(100, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime BirthDay { get; set; }
        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(150, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(20, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Required(ErrorMessage = "Поля {0} является обязательным")]
        public string PhoneNumber { get; set; }
    }
}
