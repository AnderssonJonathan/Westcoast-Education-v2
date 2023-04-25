using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education.api.Data;

namespace westcoast_education.api.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentController : ControllerBase
{
    private readonly EducationContext _context;
    public StudentController(EducationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Listar alla studenter i systemet.
    /// </summary>
    [HttpGet("listall")]
    public async Task<IActionResult> ListAll()
    {
        var result = await _context.Students
            .Select(s => new
            {
                Id = s.Id,
                SocialSecurityNumber = s.SocialSecurityNumber,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
                Adress = s.Adress,
                PostalCode = s.PostalCode,
                City = s.City,
                Country = s.Country
            })
            .ToListAsync();
            return Ok(result);
    }

    [HttpGet("student/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _context.Students
        .Select(c => new{
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,

            Courses = c.StudentCourses.Select(s => new
            {
                CourseId = s.CourseId,
                CourseTitle = s.Course.Title
            }) 
        })
        .SingleOrDefaultAsync(c => c.Id == id);
        return Ok(result);
    }
}

