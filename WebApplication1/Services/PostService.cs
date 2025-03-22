using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostsAsync(int? userId, string title)
        {
            // Build the query string based on the parameters
            var query = "?";

            if (userId.HasValue)
            {
                query += $"userId={userId.Value}&";
            }

            if (!string.IsNullOrEmpty(title))
            {
                query += $"title={Uri.EscapeDataString(title)}&";
            }

            // Remove the trailing '&' or '?' if no valid parameters
            if (query.EndsWith("&"))
            {
                query = query.Remove(query.Length - 1);
            }

            var url = "https://jsonplaceholder.typicode.com/posts" + query;

            // Call the external API
            var response = await _httpClient.GetStringAsync(url);

            // Deserialize the JSON response into a list of Post objects
            var posts = JsonConvert.DeserializeObject<List<Post>>(response);

            return posts;
        }
    }
}
