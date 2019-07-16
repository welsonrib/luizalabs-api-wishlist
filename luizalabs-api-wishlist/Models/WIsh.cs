using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Models
{
    public class Wish
    {
        public long Id { get; set; }
        public long userId { get; set; }
        public long productId { get; set; }

        public virtual Product product { get; set; }
    }
}
