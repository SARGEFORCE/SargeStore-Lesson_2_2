using SargeStoreDomain.Entities.Base.Interfaces;

namespace SargeStoreDomain.DTO.Products
{
    public class BrandDTO : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set ; }
    }
}
