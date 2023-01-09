using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Entities;
using web.Services;
using web.Models;

namespace web.Controllers 
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            postService.CreatePost(post);
            return Ok(new { message = "Post was successfully created! "});
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            return Ok(postService.GetPosts());
        }
        [HttpGet("user/{id}")]
        public IActionResult GetUserPosts(int id)
        {
            return Ok(postService.GetUserPosts(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetPost(int id) 
        {
            return Ok(postService.GetPost(id));
        }

        [Authorize(Roles = "Admin, Regular")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            postService.DeletePost(id);
            return Ok(new { message = "Post was successfully deleted!" });
        }

        [Authorize(Roles = "Admin, Regular")]
        [HttpPut("{id}")]
        public IActionResult Update(PostModel model) 
        {
            postService.UpdatePost(model);
            return Ok(new { message = "Post was successfully updated!" });
        }
    }
}
