using System;
using System.ComponentModel.DataAnnotations;

namespace PatientCardApp.Model
{
    public class TypeOfVisit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [MaxLength(20, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        public string Name { get; set; }
    }
}
