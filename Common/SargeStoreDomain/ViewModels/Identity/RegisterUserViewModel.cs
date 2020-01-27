﻿using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace SargeStoreDomain.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(256)]
        // [Remote("IsNameFree", "Account", ErrorMessage = "Пользователь с таким именем уже существует")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        //[Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
