using Dapper;
using luizalabs_api_wishlist.Models.Entities;
using luizalabs_api_wishlist.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Repositories
{
    public class WishRepository : IWishRepository
    {
        private readonly string _connectionString;
        public WishRepository(string connectionString)
        {
            _connectionString = connectionString;  
        }

        public async Task<IEnumerable<Product>> GetWishes(long userId, int pageSize, int page)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                var skip = (page - 1) * pageSize;
                return await conn.QueryAsync<Product>($"SELECT p.* FROM Wishes w LEFT JOIN Products p ON (p.id = w.productId) WHERE userId = {userId} LIMIT {skip}, {pageSize}");
            }
        }

        public async Task<Wish> GetWish(long userId, long productId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<Wish>($"SELECT * FROM Wishes WHERE userId = {userId} AND productId = {productId}");
            }
        }

        public async Task DelWish(long userId, long productId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                await conn.ExecuteAsync($"DELETE FROM Wishes WHERE userId = {userId} AND productId = {productId}");
            }
        }

        public async Task<long> AddWish(Wish wish)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Wishes(userId, productId) VALUES(@userId, @productId)";
                cmd.Parameters.AddWithValue("@userId", wish.userId);
                cmd.Parameters.AddWithValue("@producTId", wish.productId);
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return cmd.LastInsertedId;
            }
        }




    }
}
