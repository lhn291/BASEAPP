using AutoMapper;
using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs.Cart;
using BASEAPP.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.DataAccess.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ShoppingCartRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CartItemDto>> GetCartItems(Guid userId)
        {
            var shoppingCart = await _db.ShoppingCarts
                .Include(sc => sc.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId.ToString());

            if (shoppingCart == null)
            {
                return new List<CartItemDto>();
            }

            var cartItemDtos = _mapper.Map<List<CartItemDto>>(shoppingCart.CartItems);
            return cartItemDtos;
        }

        public async Task<ShoppingCart> GetShoppingCart(Guid userId)
        {
            var shoppingCart = await _db.ShoppingCarts
                .Include(sc => sc.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId.ToString());

            return shoppingCart!;
        }

        public async Task<bool> AddProductToCart(Guid userId, AddToCartDto addToCartDto)
        {
            try
            {
                var user = await _db.Users
                    .Include(u => u.ShoppingCart)
                    .ThenInclude(sc => sc.CartItems)
                    .FirstOrDefaultAsync(u => u.Id == userId.ToString());

                if (user == null)
                {
                    return false;
                }

                var shoppingCart = user.ShoppingCart;

                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCart
                    {
                        UserId = user.Id,
                        CartItems = new List<CartItem>()
                    };
                    _db.ShoppingCarts.Add(shoppingCart);
                }

                var existingCartItem = shoppingCart.CartItems
                    .FirstOrDefault(ci => ci.ProductId == addToCartDto.ProductId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += addToCartDto.Quantity;
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        ProductId = addToCartDto.ProductId,
                        Quantity = addToCartDto.Quantity,
                    };

                    shoppingCart.CartItems.Add(cartItem);
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
