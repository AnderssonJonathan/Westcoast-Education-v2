namespace westcoast_education.web.ViewModels;

public class CourseDetailsViewModel : CourseListViewModel
{
    public int CourseNumber { get; set; }
    public int Duration { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<StudentListViewModel> Students { get; set; }
    public TeacherListViewModel Teacher { get; set; }

    public Guid SelectedTeacher { get; set; }
}
