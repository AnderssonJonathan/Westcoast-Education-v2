using System.ComponentModel.DataAnnotations;

namespace westcoast_education.api.ViewModels;

public class CoursePostViewModel
{
    [Required(ErrorMessage = "Kurstitel måste anges.")]
    [StringLength(128, MinimumLength = 6)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Kursnummer måste anges.")]
    public int CourseNumber { get; set; }

    [Required(ErrorMessage = "Längd på kursen måste anges.")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "Startdatum för kurs måste anges.")]
    public DateTime StartDate { get; set; }

    public Guid? TeacherId { get; set; }
}
