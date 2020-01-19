
using SargeStoreDomain.Entities.Base.Interfaces;

namespace SargeStoreDomain.ViewModels
{
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }
    }
}