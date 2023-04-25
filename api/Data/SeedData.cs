using System.Text.Json;
using westcoast_education.api.Models;

namespace westcoast_education.api.Data.json;

public static class SeedData
{
    public static async Task LoadCourseData(EducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

         //Vill endast ladda om databsens tabell är tom...
        if(context.Courses.Any()) return;

         //Läsa in json datat...
        var json = System.IO.File.ReadAllText("Data/json/courses.json");
        
        //Konvertera json-objekten till en lista av Kurs-objekt...
        var courses = JsonSerializer.Deserialize<List<Course>>(json, options);

        if(courses is not null && courses.Count > 0)
        {
            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }

    public static async Task LoadTeacherData(EducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if(context.Teachers.Any()) return;

        var json = System.IO.File.ReadAllText("Data/json/teachers.json");
        var teachers = JsonSerializer.Deserialize<List<Teacher>>(json, options);

        if (teachers is not null && teachers.Count > 0)
        {
            await context.Teachers.AddRangeAsync(teachers);
            await context.SaveChangesAsync();
        }
    }

    public static async Task LoadStudentData(EducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if(context.Students.Any()) return;

        var json = System.IO.File.ReadAllText("Data/json/students.json");
        var students = JsonSerializer.Deserialize<List<Student>>(json, options);

        if (students is not null && students.Count > 0)
        {
            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }
    }

      public static async Task LoadStudentCourseData(EducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if(context.StudentCourse.Any()) return;

        var json = System.IO.File.ReadAllText("Data/json/studentcourse.json");
        var students = JsonSerializer.Deserialize<List<StudentCourse>>(json, options);

        if (students is not null && students.Count > 0)
        {
            await context.StudentCourse.AddRangeAsync(students);
            await context.SaveChangesAsync();
        }
    }
}

