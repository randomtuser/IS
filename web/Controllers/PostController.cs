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

        [HttpGet("user/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(postService.GetPosts(id));
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
