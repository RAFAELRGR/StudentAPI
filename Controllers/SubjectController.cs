using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;


namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            var subjects = await _subjectService.GetSubjects();
            return Ok(subjects);
        }
        [HttpGet("{SubjectId}")]
        public async Task<ActionResult<Subject>> GetSubject(int SubjectId)
        {
            var subject = await _subjectService.GetSubject(SubjectId);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpPost]
        public async Task<ActionResult<Subject>> CreateSubject(int ClassId, string SubjectName, string Subjectcode, int TeacherId)
        {
            try
            {
                var newSubject = await _subjectService.CreateSubject(ClassId, SubjectName, Subjectcode, TeacherId);
                if (newSubject == null)
                {
                    return BadRequest("No se pudo crear el estudiante");
                }
                return CreatedAtAction(nameof(GetSubject), new { SubjectId = newSubject.SubjectId }, newSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el estudiante: {ex.Message}");
            }
        }
        [HttpPut("{SubjectId}")]
        public async Task<ActionResult<Subject>> UpdateStudent(int SubjectId, int? ClassId = null,string? SubjectName = null, string? Subjectcode = null,int? TeacherId = null)
        {
            var updateSubject = await _subjectService.UpdateSubject(SubjectId, ClassId, SubjectName, Subjectcode, TeacherId);
            if (updateSubject == null)
            {
                return NotFound();
            }
            return Ok(updateSubject);
        }
        [HttpDelete("{SubjectId}")]
        public async Task<ActionResult<Subject>> DeleteSubject(int SubjectId, [FromQuery] bool active)
        {
            var subject = await _subjectService.DeleteSubject(SubjectId, active);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
    }
}
