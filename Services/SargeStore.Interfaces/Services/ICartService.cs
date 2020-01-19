using System;
using SargeStoreDomain.ViewModels;

namespace SargeStore.Interfaces.Services
{
    public interface ICartService
    {
        void AddToCart(int id);
        void DecrementFromCart(int id);
        void RemoveFromCart(int id);
        void RemoveAll();
        
        CartViewModel TransformFromCart();
    }
}
