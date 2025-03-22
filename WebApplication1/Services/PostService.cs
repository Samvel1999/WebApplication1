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
            var query = "?";

            if (userId.HasValue)
            {
                query += $"userId={userId.Value}&";
            }

            if (!string.IsNullOrEmpty(title))
            {
                query += $"title={Uri.EscapeDataString(title)}&";
            }

            if (query.EndsWith("&"))
            {
                query = query.Remove(query.Length - 1);
            }

            var url = "https://jsonplaceholder.typicode.com/posts" + query;
            var response = await _httpClient.GetStringAsync(url);
            var posts = JsonConvert.DeserializeObject<List<Post>>(response);

            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {

            var url = $"https://jsonplaceholder.typicode.com/posts/{id}";
            var response = await _httpClient.GetStringAsync(url);
            var post = JsonConvert.DeserializeObject<Post>(response);

            return post;
        }

        public async Task<bool> DeletePostByIdAsync(int id)
        {

            var url = $"https://jsonplaceholder.typicode.com/posts/{id}";
            var response = await _httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }
}
