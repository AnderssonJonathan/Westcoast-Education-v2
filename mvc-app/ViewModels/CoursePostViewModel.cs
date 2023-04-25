using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace westcoast_education.web.ViewModels;

public class CoursePostViewModel
{
    [Required(ErrorMessage = "Kursnamn är obligatoriskt")]
    [DisplayName("Kursnamn")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Kursbeteckning är obligatoriskt")]
    [DisplayName("Kursbeteckning")]
    public int CourseNumber { get; set; }

    [Required(ErrorMessage = "Längd på kursen är obligatoriskt")]
    [DisplayName("Antal veckor")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "Startdatum är obligatoriskt")]
    [DisplayName("Startdatum")]
    public DateTime StartDate { get; set; }

    [DisplayName("Teacher")]
    public Guid? TeacherId { get; set; }
}
