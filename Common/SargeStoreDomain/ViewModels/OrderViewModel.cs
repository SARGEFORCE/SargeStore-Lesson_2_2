using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SargeStoreDomain.ViewModels
{
    /// <summary>Модель-представление заказа</summary>
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Имя является обязательным")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона для связи")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Неизвестно куда везти!")]
        public string Address { get; set; }
    }
}
