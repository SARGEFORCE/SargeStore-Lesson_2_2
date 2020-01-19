using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SargeStoreDomain.ViewModels
{
    public class EmployeeView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина имени должна находиться в пределах от 2 до 20 символов")]
        [RegularExpression(@"(?:[А-ЯЁ][а-яё]+)|(?:[A-Z][a-z]+)", ErrorMessage ="Странное имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательной", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина фамилии должна находиться в пределах от 2 до 20 символов")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Не указан возраст!")]
        public int Age { get; set; }
    }
}
