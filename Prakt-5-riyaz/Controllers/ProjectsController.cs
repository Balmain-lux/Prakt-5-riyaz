
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prakt_5_riyaz.Data;
using Prakt_5_riyaz.Models;
namespace Prakt_5_riyaz.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/tasks")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjectTasks(int id)
        {
            var tasks = await _context.Tasks
                .Where(t => t.ProjectID == id)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<object>> GetProjectStats(int id)
        {
            var tasks = await _context.Tasks
                .Where(t => t.ProjectID == id)
                .ToListAsync();

            var stats = new
            {
                TotalTasks = tasks.Count,
                CompletedTasks = tasks.Count(t => t.Status == "Done"),
                OverdueTasks = tasks.Count(t => t.Deadline < DateTime.Now && t.Status != "Done")
            };

            return Ok(stats);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}

