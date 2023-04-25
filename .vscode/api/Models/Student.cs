namespace westcoast_education.api.Models;

public class Student : Person
{
    // Navigeringsegenskaper
   public ICollection<StudentCourse> StudentCourses { get; set; }
}
