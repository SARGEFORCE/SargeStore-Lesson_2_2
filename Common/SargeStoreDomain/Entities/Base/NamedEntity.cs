using SargeStoreDomain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SargeStoreDomain.Entities.Base
{
    /// <summary>Именованная сущность</summary>
    public abstract class NamedEntity : BaseEntity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }


}
