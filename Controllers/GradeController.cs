using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;



namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Grade>>> GetGrades()
        {
            try
            {
                var grades = await _gradeService.GetGrades();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las calificaciones: {ex.Message}");
            }
        }

        [HttpGet("{GradeId}")]
        public async Task<ActionResult<Grade>> GetGrade(int GradeId)
        {
            try
            {
                var grade = await _gradeService.GetGrade(GradeId);
                if (grade == null)
                {
                    return NotFound();
                }
                return Ok(grade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la calificación: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> CreateStudent(int StudentId, int SubjectId,float Score, string Description)
        {
            try
            {
                var newGrade = await _gradeService.CreateGrade(StudentId,SubjectId, Description, Score);
                if (newGrade == null)
                {
                    return BadRequest("No se pudo crear el estudiante");
                }
                return CreatedAtAction(nameof(GetGrade), new { GradeId = newGrade.GradeId }, newGrade);
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el estudiante: {ex.Message}");
            }
        }

        [HttpPut("{GradeId}")]
        public async Task<ActionResult<Grade>> UpdateGrade(int GradeId, int? StudentId = null, int? SubjectId = null, float? Score = null, string? Description = null)
        {
            try
            {
                var updatedGrade = await _gradeService.UpdateGrade(GradeId, StudentId, SubjectId, Score, Description);
                if (updatedGrade == null)
                {
                    return NotFound();
                }
                return Ok(updatedGrade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la calificación: {ex.Message}");
            }
        }



        [HttpDelete("{GradeId}")]
        public async Task<ActionResult<Student>> DeleteStudent(int GradeId, [FromQuery] bool ActiveGrade)
        {
            var grade = await _gradeService.DeleteGrade(GradeId, ActiveGrade);
            if (grade == null)
            {
                return NotFound();
            }
            return Ok(grade);
        }
    }
}
