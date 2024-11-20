using AmazonMVC.Data;
using AmazonMVC.DTO;
using AmazonMVC.Models;
using AmazonMVC.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonMVC.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CartRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetCartDTO>> DisplayCart(string UserId)
        {
            var getDb = await _context.CartItems.Where(ci => ci.UserId == UserId).ToListAsync();

            var getCart = getDb.Select(ci => _mapper.Map<GetCartDTO>(ci)).ToList();

            return getCart;


        }
        public async Task<GetCartDTO> AddItemsToCart(AddCartDTO cartItems)
        {
            var findProduct = await _context.Products.FindAsync(cartItems.ProductId);

            if (findProduct.StockQuantity < cartItems.Quantity)
            {
                throw new ArgumentException($"You choosed to many products");
            }

            findProduct.StockQuantity -= cartItems.Quantity;

            var cartitem = _mapper.Map<CartItem>(cartItems);

            cartitem.Price = findProduct.Price * cartitem.Quantity;

            await _context.CartItems.AddAsync(cartitem);

            await _context.SaveChangesAsync();

            return _mapper.Map<GetCartDTO>(cartitem);
        }

        public async Task DeleteCartItem(DeleteCartDTO cartItem)
        {
            var findItem = await _context.CartItems.FindAsync(cartItem.Id);

            if (findItem != null)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == findItem.ProductId);

                if (product != null)
                {
                    product.StockQuantity += findItem.Quantity;

                    _context.CartItems.Remove(findItem);

                    await _context.SaveChangesAsync();
                }



            }
        }

        public async Task ClearCart(string userId)
        {
            var itemsToRemove = _context.CartItems.Where(ci => ci.UserId == userId).ToList();

            foreach (var item in itemsToRemove)
            {
                var itemToReturn = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (itemToReturn != null)
                {
                    itemToReturn.StockQuantity += item.Quantity;
                }
            }

            _context.CartItems.RemoveRange(itemsToRemove);

            await _context.SaveChangesAsync();

        }
    }
}
