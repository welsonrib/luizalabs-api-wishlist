using Dapper;
using luizalabs_api_wishlist.Models.Entities;
using luizalabs_api_wishlist.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;  
        }

        public async Task<IEnumerable<Product>> GetProducts(int pageSize, int page)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                var skip = (page - 1) * pageSize;
                return await conn.QueryAsync<Product>($"SELECT * FROM Products LIMIT {skip}, {pageSize}");
            }
        }

        public async Task<Product> GetProduct(long id)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<Product>($"SELECT * FROM Products WHERE id = {id}");
            }
        }

        public async Task<long> AddProduct(Product product)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Products(name) VALUES(@name)";
                cmd.Parameters.AddWithValue("@name", product.name);
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
                return cmd.LastInsertedId;
            }
        }




    }
}
