using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Services;
using web.Entities;
using web.Models;

namespace web.Controllers 
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase 
    {
        private ICommentService commentService;
        CommentController(ICommentService commentService) 
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public IActionResult Create(Comment comment) 
        {
            commentService.CreateComment(comment);
            return Ok(new { message = "Comment has been successfully created!" });
        }

        [Authorize(Roles = "Admin, Regular")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            commentService.DeleteComment(id);
            return Ok(new { message = "Comment has been successfully deleted!" });
        }

        [Authorize(Roles = "Admin, Regular")]
        [HttpPut("{id}")]
        public IActionResult Update(CommentModel model) 
        {
            commentService.UpdateComment(model);
            return Ok(new { message = "Comment has been successfully updated!" });
        }
        
    }
}