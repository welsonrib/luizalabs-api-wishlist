
namespace luizalabs_api_wishlist.Models.Entities
{
    public class Wish
    {
        public long Id { get; set; }
        public long userId { get; set; }
        public long productId { get; set; }

        public virtual User user { get; set; }
        public virtual Product product { get; set; }
    }
}
