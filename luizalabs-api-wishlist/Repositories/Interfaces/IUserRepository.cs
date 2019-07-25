using luizalabs_api_wishlist.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers(int pageSize, int page);
        Task<User> GetUser(long id);
        Task<long> AddUser(User user);
    }
}
