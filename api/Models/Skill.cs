namespace westcoast_education.api.Models
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}