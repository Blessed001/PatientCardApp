using System.ComponentModel.DataAnnotations;

namespace PatientCardApp.Model
{
    public class Gender
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поля {0} является обязательным")]
        [Display(Name = "Пол")]
        public string Name { get; set; }
    }
}
