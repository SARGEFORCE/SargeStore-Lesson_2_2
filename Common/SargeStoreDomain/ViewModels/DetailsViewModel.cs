﻿namespace SargeStoreDomain.ViewModels
{
    /// <summary>
    /// Полная модель-представление для корзины, 
    /// где есть область корзины и область оформления заказа
    /// </summary>
    public class DetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
    }
}
