namespace PRN231_AS1_API.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; } 
        public string LecturerName { get; set; }
        public string Phone { get; set; } 

        public List<LecturerCourse> LecturerCourses { get; set; }
    }
}
