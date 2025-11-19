namespace Prakt_5_riyaz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Prakt_5_riyaz.Data;
    using Prakt_5_riyaz.Models;

    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/tasks/{taskId}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetTaskComments(int taskId)
        {
            var comments = await _context.Comments
                .Where(c => c.TaskId == taskId)
                .Include(c => c.Employee)
                .ToListAsync();

            return Ok(comments);
        }

        [HttpPost("/api/tasks/{taskId}/comments")]
        public async Task<ActionResult<Comment>> PostComment(int taskId, Comment comment)
        {
            comment.TaskId = taskId;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskComments", new { taskId = taskId }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
