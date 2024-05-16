namespace PRN231_AS1_API.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set;}
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string SchoolYear { get; set; }
        public bool IsStudy { get; set; }   
        public List<Grade> Grades { get; set; }
    }
}
