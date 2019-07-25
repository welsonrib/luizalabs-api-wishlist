using Dapper;
using luizalabs_api_wishlist.Models.Entities;
using luizalabs_api_wishlist.Models.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;  
        }

        public async Task<IEnumerable<User>> GetUsers(int pageSize, int page)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                var skip = (page - 1) * pageSize;
                return await conn.QueryAsync<User>($"SELECT * FROM Users LIMIT {skip}, {pageSize}");
            }
        }

        public async Task<User> GetUser(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<User>($"SELECT * FROM Users WHERE id = {id}");
            }
        }

        public async Task<long> AddUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Users(name, email) VALUES(@name, @email)";
                cmd.Parameters.AddWithValue("@name", user.name);
                cmd.Parameters.AddWithValue("@email", user.email);
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return cmd.LastInsertedId;
            }
        }




    }
}
