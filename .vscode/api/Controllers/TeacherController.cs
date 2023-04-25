using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education.api.Data;

namespace westcoast_education.api.Controllers;

[ApiController]
[Route("api/v1/teachers")]
public class TeacherController : ControllerBase
{
    private readonly EducationContext _context;
    public TeacherController(EducationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Listar alla lärare i systemet.
    /// </summary>
    [HttpGet("listall")]
    public async Task<IActionResult> ListAll()
    {
        var result = await _context.Teachers
            .Select(t => new
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName
            })
            .ToListAsync();
            return Ok(result);
    }

    [HttpGet("teacher/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _context.Teachers
        .Select(c => new{
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,

            Courses = c.Courses.Select(s => new
            {
                CourseId = s.CourseId,
                Title = s.Title
            })
        })
        .SingleOrDefaultAsync(c => c.Id == id);
        return Ok(result);
    }


}
