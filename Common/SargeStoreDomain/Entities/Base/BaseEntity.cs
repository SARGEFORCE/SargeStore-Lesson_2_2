using SargeStoreDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SargeStoreDomain.Entities.Base
{
    /// <summary>Сущность</summary>
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Огшраничение уникальности
        public int Id { get; set; }
    }
}
