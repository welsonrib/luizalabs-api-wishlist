using luizalabs_api_wishlist.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Repositories.Interfaces
{
    public interface IWishRepository
    {
        Task<IEnumerable<Product>> GetWishes(long userId, int pageSize, int page);
        Task<Wish> GetWish(long userId, long productId);
        Task DelWish(long userId, long productId);
        Task<long> AddWish(Wish wish);
    }
}
