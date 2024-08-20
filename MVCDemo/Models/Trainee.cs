namespace MVCDemo.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Grade { get; set; }

        public int DeptId { get; set; }
        public Department Department { get; set; }

        // Navigation properties
        public ICollection<CourseResult> CourseResults { get; set; }
    }
}
