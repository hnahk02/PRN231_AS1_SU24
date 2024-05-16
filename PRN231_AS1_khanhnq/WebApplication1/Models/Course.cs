namespace PRN231_AS1_API.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public List<LecturerCourse> LecturerCourses { get; set; }   
        public List<Grade> Grades { get; set; }
    }
}
