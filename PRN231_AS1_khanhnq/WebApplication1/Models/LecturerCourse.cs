namespace PRN231_AS1_API.Models
{
    public class LecturerCourse
    {
        public int LecturerId { get; set; }    
        public Lecturer Lecturer { get; set; }  

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
