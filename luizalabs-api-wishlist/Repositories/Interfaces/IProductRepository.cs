using luizalabs_api_wishlist.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int pageSize, int page);
        Task<Product> GetProduct(long id);
        Task<long> AddProduct(Product user);
    }
}
