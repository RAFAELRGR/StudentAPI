using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;
using System.Globalization;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var students = await _studentService.getStudents();
            return Ok(students);
        }
        [HttpGet("{StudentId}")]
        public async Task<ActionResult<Student>> GetStudent(int StudentId)
        {
            var student = await _studentService.getStudent(StudentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(string StudentName, string StudentLastName, string Email, string Birthdate)
        {
            try
            {
                if (!DateTime.TryParseExact(Birthdate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return BadRequest("Formato de fecha incorrecto. Debe ser dd-MM-yyyy.");
                }
                var dateOnlyBirthdate = new DateOnly(parsedDate.Year, parsedDate.Month, parsedDate.Day);
                var newStudent = await _studentService.createStudent(StudentName, StudentLastName, Email, dateOnlyBirthdate);

                if (newStudent == null)
                {
                    return BadRequest("No se pudo crear el estudiante");
                }

                return CreatedAtAction(nameof(GetStudent), new { StudentId = newStudent.StudentId }, newStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el estudiante: {ex.Message}");
            }
        }
        [HttpPut("{StudentId}")]
        public async Task<ActionResult<Student>> UpdateStudent(int StudentId, string? StudentName = null, string? StudentLastName = null, string? Email = null, string? Birthdate = null)
        {
            DateOnly parsedBirthdate;
            if (Birthdate != null)
            {
                if (!DateOnly.TryParseExact(Birthdate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedBirthdate))
                {
                    return BadRequest("Formato de fecha de nacimiento no válido. Utilice el formato dd-MM-yyyy.");
                }
            }
            else
            {
                parsedBirthdate = DateOnly.MinValue;
            }
            var updatedStudent = await _studentService.updateStudent(StudentId, StudentName, StudentLastName, Email, parsedBirthdate);
            if (updatedStudent == null)
            {
                return NotFound();
            }
            return Ok(updatedStudent);
        }
        [HttpDelete("{StudentId}")]
        public async Task<ActionResult<Student>> DeleteStudent(int StudentId, [FromQuery] bool active)
        {
            var student = await _studentService.deleteStudent(StudentId, active);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
