namespace PRN231_AS1_API.Models
{
    public class Grade
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }   
        public Course Course { get; set;}

        public double HighestScore { get; set; }    
    }
}
