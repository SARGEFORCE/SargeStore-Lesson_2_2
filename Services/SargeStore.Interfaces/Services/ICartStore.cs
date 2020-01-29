using SargeStoreDomain.Models;

namespace SargeStore.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
