using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education.api.Data;
using westcoast_education.api.Models;
using westcoast_education.api.ViewModels;

namespace westcoast_education.api.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CoursesController : ControllerBase
{
    private readonly EducationContext _context;
    public CoursesController(EducationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Listar alla kurser i systemet.
    /// </summary>
    [HttpGet("listall")]
    public async Task<IActionResult> ListAll()
    {
        var result = await _context.Courses
            .Select(c => new
            {
                CourseId = c.CourseId,
                Title = c.Title,
                StartDate = c.StartDate.ToShortDateString(),
                Teacher = c.Teacher != null ? c.Teacher.FirstName + " " + c.Teacher.LastName : "Saknas",
                Students = c.StudentCourses.Select(s => new{
                    StudentId = s.StudentId,
                    Name = s.Student.FirstName + " " + s.Student.LastName
                }).ToList()
            })
            .ToListAsync();
        return Ok(result);
    }

    /// <summary>
    /// Hämtar en kurs baserat på kursid.
    /// </summary>
    /// <response code="200">
    /// Returnerar en kurs med kursinformation baserat på sökt kurs med dess lärare och studenter
    /// </response>
    [HttpGet("course/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _context.Courses
        .Select(c => new{
            CourseId = c.CourseId,
            Title = c.Title,
            StartDate = c.StartDate.ToShortDateString(),
            EndDate = c.EndDate.ToShortDateString(),
            CourseNumber = c.CourseNumber,
            Teacher = c.Teacher != null ? new {
                Id = c.Teacher.Id,
                FirstName = c.Teacher.FirstName,
                LastName = c.Teacher.LastName,
                Email = c.Teacher.Email,
                Phone = c.Teacher.Phone
            } : null,
            Students = c.StudentCourses.Select(s => new
            {
                Id = s.Student.Id,
                FirstName = s.Student.FirstName,
                LastName = s.Student.LastName,
                Email = s.Student.Email,
                Phone = s.Student.Phone,
                Status = ((CourseStatusEnum)s.Status).ToString()
            }) 
        })
        .SingleOrDefaultAsync(c => c.CourseId == id);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Add(CoursePostViewModel model)
    {
        var exists = await _context.Courses.SingleOrDefaultAsync(
            c => c.CourseNumber == model.CourseNumber &&
            c.StartDate == model.StartDate
        );

        if(exists is not null) return BadRequest($"Kurs med kursnummer {model.CourseNumber} och kursstart {model.StartDate.ToShortDateString()} finns redan i systemet.");
        
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Title = model.Title,
            CourseNumber = model.CourseNumber,
            Duration = model.Duration,
            StartDate = model.StartDate,
            TeacherId = model.TeacherId,
            EndDate = model.StartDate.AddDays(model.Duration * 7)
        };

        await _context.Courses.AddAsync(course);

        if(await _context.SaveChangesAsync() > 0){
            var result = new{
                CourseId = course.CourseId,
                Title = course.Title,
                StartDate = course.StartDate.ToShortDateString(),
                Enddate = course.EndDate.ToShortDateString()
            };
            return CreatedAtAction(nameof(GetById), new { Id = course.CourseId }, result); 
        }
        return StatusCode(500, "Internal Server Error");
    }

    [HttpPatch("addteacher/{courseId}/{teacherId}")]
    public async Task<IActionResult> AddTeacherToCourse(Guid courseId, Guid teacherId)
    {
        var course = await _context.Courses.FindAsync(courseId);

        if (course is null) return BadRequest("Kunde inte hitta kursen i systemet.");

        var teacher = await _context.Teachers.FindAsync(teacherId);

        if(teacher is null) return BadRequest($"Kunde inte hitta läraren i systemet.");

       course.Teacher = teacher;

       _context.Courses.Update(course);

        if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }

        return StatusCode(500, "Internal Server Error");
    }

}
