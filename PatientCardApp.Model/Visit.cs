using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCardApp.Model
{
   public class Visit
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Display(Name = "Дата посещения")]
        public DateTime DayOfVisit { get; set; }

        //[Required(ErrorMessage = "Поля {0} является обязательным")]
        //[MaxLength(20, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        //[Display(Name = "Типа посещения")]
        //public string TypeOfVisit { get; set; }

        [MaxLength(1500, ErrorMessage = "Поля {0} должен быть максимуме {1} символи")]
        [Display(Name = "Диагноз")]
        public string Diagnosis { get; set; }

        public int PatientCardId { get; set; }

        public PatientCard PatientCard { get; set; }
    }
}
