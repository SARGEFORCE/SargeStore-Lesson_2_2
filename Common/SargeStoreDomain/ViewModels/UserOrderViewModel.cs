﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SargeStoreDomain.ViewModels
{
    public class UserOrderViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name="Название")]
        public string Name { get; set; }

        [Display(Name="Телефон для связи")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name="Адрес доставки")]
        public string Address { get; set; }

        [Display(Name="Полная стоимость")]
        public decimal TotalSum { get; set; }
    }
}
