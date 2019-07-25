using Newtonsoft.Json;

namespace luizalabs_api_wishlist.Models.Responses
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
