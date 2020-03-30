using System;
using System.ComponentModel.DataAnnotations;

namespace PatientCardApp.Model
{
    public class Visit
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Display(Name = "Дата посещения")]
        public DateTime DayOfVisit { get; set; }

        [MaxLength(1500, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Display(Name = "Диагноз")]
        public string Diagnosis { get; set; }

        public int PatientCardId { get; set; }
        public int? TypeOfVisitId { get; set; }


        public PatientCard PatientCard { get; set; }

        public TypeOfVisit TypeOfVisit { get; set; }
    }
}
