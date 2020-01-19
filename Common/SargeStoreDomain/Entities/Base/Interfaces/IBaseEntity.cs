using System;
using System.Collections.Generic;
using System.Text;

namespace SargeStoreDomain.Entities.Base.Interfaces
{
    /// <summary>Сущность</summary>
    public interface IBaseEntity
    {
        /// <summary>Идентификатор</summary>
        int Id { get; set; }
    }
}
