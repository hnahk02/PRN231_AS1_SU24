using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_AS1_API.Models;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace PRN231_AS1_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PRN231_AS1 : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PRN231_AS1(ApplicationDBContext context)
        {
            _context = context;
        }

        #region API Course
        // GET: api/PRN231_AS1/Courses
        [HttpGet("getAllCourses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _context.Courses
                 .Select(c => new { c.CourseId, c.CourseName })
                 .ToListAsync();

            return Ok(courses);
        }

        // GET: api/Courses/5
        [HttpGet("Courses/{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // POST: api/Courses
        [HttpPost("Courses")]
        public async Task<ActionResult<Course>> PostCourse([FromBody] string courseName)
        {
            if (string.IsNullOrEmpty(courseName))
            {
                return BadRequest("Course name cannot be empty");
            }

            try
            {
              
                var course = new Course
                {
                    CourseName = courseName
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

               
                return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating course: {ex.Message}");
            }
        }

        // PUT: api/Courses/{id}
        [HttpPut("Courses/{id}")]
        public async Task<IActionResult> PutCourse(int id, [FromBody] string courseName)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

           
            existingCourse.CourseName = courseName;

            _context.Entry(existingCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound($"Course with ID {id} not found during concurrency check.");
                }
                else
                {
                    throw; 
                }
            }

            return NoContent();
        }

        // DELETE: api/Courses/5
        [HttpDelete("Courses/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region API Student
        // GET: api/PRN231_AS1/Students
        [HttpGet("getAllStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new { 
                    s.StudentId, 
                    s.StudentName, 
                    Dob = s.Dob.ToString("yyyy-MM-dd"), 
                    s.Gender, 
                    s.SchoolYear, 
                    s.IsStudy
                })
                .ToListAsync();
            return Ok(students);
        }

        // GET: api/Courses/5
        [HttpGet("Students/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // POST: api/Courses
        [HttpPost("Student")]
        public async Task<ActionResult<Student>> PostStudent(string Name,
                DateTime Dob, string Gender, string SchoolYear, bool IsStudy)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Course name cannot be empty");
            }

            try
            {

                var student = new Student
                {
                    StudentName = Name,
                    Dob = Dob,
                    Gender = Gender,
                    SchoolYear = SchoolYear,
                    IsStudy = IsStudy
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating course: {ex.Message}");
            }

        }

        // PUT: api/Courses/{id}
        [HttpPut("Student/{id}")]
        public async Task<IActionResult> PutStudent(int id,string Name,
                DateTime Dob, string Gender, string SchoolYear, bool IsStudy)
        {
            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            if(String.IsNullOrEmpty(Name))
            {
                existingStudent.StudentName = Name;
            }

            if(Dob.ToString() != null)
            {
                existingStudent.Dob = Dob;
            }

            if (String.IsNullOrEmpty(Gender))
            {
                existingStudent.Gender = Gender;
            }

            if(String.IsNullOrEmpty(SchoolYear))
            {
                existingStudent.SchoolYear = SchoolYear;
            }

            _context.Entry(existingStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound($"Student with ID {id} not found during concurrency check.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("Student/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region API Lecturer
        // GET: api/PRN231_AS1/Lecturers
        [HttpGet("Lecturers")]
        public async Task<ActionResult<IEnumerable<Lecturer>>> GetLecturer()
        {
            var lecturers = await _context.Lecturers
                .Select(l => new
                {
                    l.LecturerId,
                    l.LecturerName,
                    l.Phone
                })
                .ToListAsync();
            return Ok(lecturers);   
        }

        // GET: api/Courses/5
        [HttpGet("Lecturers/{id}")]
        public async Task<ActionResult<Lecturer>> GetLecturer(int id)
        {
            var lecture = await _context.Lecturers.FindAsync(id);

            if (lecture == null)
            {
                return NotFound();
            }

            return lecture;
        }

        // POST: api/Courses
        [HttpPost("Lecturers")]
        public async Task<ActionResult<Lecturer>> PostLecturer(string Name, string Phone )
        {
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Course name cannot be empty");
            }

            try
            {

                var lecturer = new Lecturer
                {
                    LecturerName = Name,
                    Phone = Phone
                };

                _context.Lecturers.Add(lecturer);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetCourse), new { id = lecturer.LecturerId }, lecturer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating lecturer: {ex.Message}");
            }
        }

        // PUT: api/Courses/{id}
        [HttpPut("Lectuers/{id}")]
        public async Task<IActionResult> PutLecturer(int id, string Name, string Phone)
        {
            var existingLecturer = await _context.Lecturers.FindAsync(id);
            if (existingLecturer == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }


            existingLecturer.LecturerName = Name;

            _context.Entry(existingLecturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound($"Lecturer with ID {id} not found during concurrency check.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Courses/5
        [HttpDelete("Lectuers/{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        // GET: api/PRN231_AS1/LecturerCourses
        [HttpGet("LecturerCourses")]
        public async Task<ActionResult<IEnumerable<LecturerCourse>>> GetLecturerCourses()
        {
            var lecturercoures = await _context.LecturerCourses
                .Include(lc => lc.Lecturer)
                .Include(lc => lc.Course)
                .Select(lc => new
                {
                    LecturerName = lc.Lecturer.LecturerName,
                    CourseName = lc.Course.CourseName,
                })
                .ToListAsync();
            return Ok(lecturercoures);
        }

        // GET: api/PRN231_AS1/Grades
        [HttpGet("Grade")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            var grades = await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Select(g => new
                {
                    CourseName = g.Course.CourseName,
                    StudentName = g.Student.StudentName,
                    g.HighestScore
                })
                .ToListAsync();

            return Ok(grades);
        }

        //3
        // GET: api/LecturerCourse
        [HttpGet("GetLecturerByCourseName")]
        public async Task<ActionResult<IEnumerable<LecturerCourse>>> GetLecturerByCourseName([FromQuery]string courseName)
        {
            if (string.IsNullOrEmpty(courseName))
            {
              

                return Ok(GetLecturerCourses());
            }
            else
            {
                // If courseName is provided, filter lecturer courses by courseName
                var lecturerCourses = await _context.LecturerCourses
                    .Include(lc => lc.Lecturer)
                    .Include(lc => lc.Course)
                    .Where(lc => lc.Course.CourseName == courseName)
                    .Select(lc => new
                    {
                        LecturerName = lc.Lecturer.LecturerName,
                        CourseName = lc.Course.CourseName
                    })
                    .ToListAsync();

                return Ok(lecturerCourses);
            }
        }

        //4


        //5
        //3
     




        //Check 
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }

        private bool LecturerExist(int id)
        {
            return _context.Lecturers.Any(e => e.LecturerId == id);
        }

        private bool StudentExist(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }


    }
}
