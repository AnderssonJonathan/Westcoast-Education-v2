namespace westcoast_education.api.Models;

public class Teacher : Person
{
    // Navigerings egenskaper
    // Att en lärare kan undervisa i flera kurser...
    public ICollection<Course> Courses { get; set; }
    
    // Att en lärare kan ha flera färdigheter...
    public ICollection<Skill> Skills { get; set; }
}
