using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PatientCardApp.Model
{
    public class PatientCard
    {
        public PatientCard()
        {
            Visits = new Collection<Visit>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(20, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(20, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Display(Name = "Отчество")]
        public string MidleName { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(20, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Дата Рождения")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(30, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Display(Name = "Адресс")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(20, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }
        public int? GenderId { get; set; }
        public Gender Gender { get; set; }

        public ICollection<Visit> Visits { get; set; }

    }
}
