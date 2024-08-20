namespace MVCDemo.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string MinDegree { get; set; }

        public int DeptId { get; set; }
        public Department Department { get; set; }

        // Navigation properties
        public ICollection<Instructor> Instructors { get; set; }
        public ICollection<CourseResult> CourseResults { get; set; }
    }
}
