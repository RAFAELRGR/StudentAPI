using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;

namespace StudentApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            var teachers = await _teacherService.GetTeachers();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> CreateTeacher(string TeacherName, string TeacherLastName,string TeacherEmail, string TeacherPhone)
        {
            try
            {
                var newTeacher = await _teacherService.CreateTeacher(TeacherName, TeacherLastName, TeacherEmail, TeacherPhone);
                if (newTeacher == null)
                {
                    return BadRequest("No se pudo crear el Profesor");
                }
                return CreatedAtAction(nameof(GetTeacher), new { id = newTeacher.TeacherId }, newTeacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el profesor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id,string TeacherName = null, string TeacherLastName =null , string TeacherEmail = null, string TeacherPhone = null)
        {
            var existingTeacher = await _teacherService.GetTeacher(id);
            if (existingTeacher == null)
            {
                return NotFound();
            }

            var updatedTeacher = await _teacherService.UpdateTeacher(id, TeacherName, TeacherLastName, TeacherEmail, TeacherPhone);
            if (updatedTeacher == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id, [FromQuery] bool active)
        {
            var teacher = await _teacherService.DeleteTeacher(id, active);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }
    }
}
