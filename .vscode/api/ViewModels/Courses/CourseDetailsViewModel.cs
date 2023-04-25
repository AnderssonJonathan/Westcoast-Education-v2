using westcoast_education.api.Models;

namespace westcoast_education.api.ViewModels;

public class CourseDetailsViewModel
{
    public int CourseNumber { get; set; }
    public int Duration { get; set; }
    public DateTime EndDate { get; set; }
}
