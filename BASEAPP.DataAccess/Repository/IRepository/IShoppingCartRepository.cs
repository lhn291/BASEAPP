using BASEAPP.Models.DTOs.Cart;
using BASEAPP.Models.Models;

namespace BASEAPP.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCart(Guid userId);
        Task<List<CartItemDto>> GetCartItems(Guid userId);
        Task<bool> AddProductToCart(Guid userId, AddToCartDto addToCartDto);
    }
}
