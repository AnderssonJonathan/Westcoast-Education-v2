using Microsoft.AspNetCore.Mvc.Rendering;

namespace westcoast_education.web.ViewModels
{
    public class CourseEditViewModel
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int CourseNumber { get; set; }
        
        //public List<SelectListItem> Teachers { get; set; }
    }
}