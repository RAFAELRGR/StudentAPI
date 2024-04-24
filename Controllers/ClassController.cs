

using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Class>>> GetClasses()
        {
            return Ok(await _classService.GetClasses());
        }
        [HttpGet("{ClassId}")]
        public async Task<ActionResult<Class>> GetClass(int ClassId)
        {
            var clase = await _classService.GetClass(ClassId);
            if (clase == null)
            {
                return NotFound("Clase no encontrada");
            }
            return Ok(clase);
        }
        [HttpPost]
        public async Task<ActionResult<Class>> CreateClass(string ClassName)
        {
            try
            {
                var createdClass = await _classService.createClass(ClassName);
                if (createdClass == null)
                {
                    return BadRequest("No se pudo crear la clase");
                }
                return CreatedAtAction(nameof(GetClass), new { ClassId = createdClass.ClassId }, createdClass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la asistencia: {ex.Message}");
            }
        }


        [HttpPut("{ClassId}")]
        public async Task<ActionResult<Class>> UpdateStudent(int ClassId, string? ClassName = null )
        {
            var updatedClass = await _classService.updateClass(ClassId, ClassName);
            if (updatedClass == null)
            {
                return NotFound();
            }
            return Ok(updatedClass);
        }

        [HttpDelete("{ClassId}")]
        public async Task<ActionResult<Student>> DeleteClass(int ClassId, [FromQuery] bool ActiveClass)
        {
            var student = await _classService.deleteClass(ClassId, ActiveClass);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
