﻿using SargeStoreDomain.Entities.Base.Interfaces;

namespace SargeStoreDomain.ViewModels
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}