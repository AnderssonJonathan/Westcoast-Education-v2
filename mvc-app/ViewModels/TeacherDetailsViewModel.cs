namespace westcoast_education.web.ViewModels;

public class TeacherDetailsViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public ICollection<CourseListViewModel> Courses { get; set; }
}
