using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;


namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts?userId=1&title=qui%20est%20esse
        [HttpGet("posts")]
        public async Task<ActionResult<List<Post>>> GetPosts([FromQuery] int? userId, [FromQuery] string title)
        {
            var posts = await _postService.GetPostsAsync(userId, title);

            // Return the posts
            return posts;
        }

        // GET: api/posts?userId=1&title=qui%20est%20esse
        [HttpGet("posts/{id}")]
        public async Task<ActionResult<Post>> GetByIdPosts(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            // Return the posts
            return post;
        }
    }
}
