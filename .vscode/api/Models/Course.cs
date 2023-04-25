using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_education.api.Models;

public class Course
{
    public Guid CourseId { get; set; }  
    public string Title { get; set; }
    public int CourseNumber { get; set; }
    public int Duration { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigeringsegenskaper...
    // Aggregation
    public ICollection<StudentCourse> StudentCourses { get; set; }

    // Composition 
    public Guid? TeacherId { get; set; } // Guid sätts till nullable vilket möjliggör att lärare kan registreras utan att samtidigt behöva knyta läraren till en specifik kurs.
    public Teacher Teacher { get; set; }
}
