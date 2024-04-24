using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using StudentApplication.Services;
using System.Globalization;


namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Attendance>>> GetAttendances()
        {
            try
            {
                var attendances = await _attendanceService.GetAttendances();
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las asistencias: {ex.Message}");
            }
        }
        [HttpGet("{AttendanceId}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int AttendanceId)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendance(AttendanceId);
                if (attendance == null)
                {
                    return NotFound();
                }
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la asistencia: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Attendance>> CreateAttendance(int StudentId, int SubjectId, string Date, bool AttendanceFlag)
        {
            DateOnly parsedDate;
            if (!DateOnly.TryParseExact(Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return BadRequest("Formato de fecha no válido. Utilice el formato dd-MM-yyyy.");
            }
            try
            {
                var newAttendance = await _attendanceService.CreateAttendance(StudentId, SubjectId, parsedDate, AttendanceFlag);
                if (newAttendance == null)
                {
                    return BadRequest("No se pudo crear la asistencia");
                }
                return CreatedAtAction(nameof(GetAttendance), new { AttendanceId = newAttendance.AttendanceId }, newAttendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la asistencia: {ex.Message}");
            }
        }
        [HttpPut("{AttendanceId}")]
        public async Task<ActionResult<Attendance>> UpdateAttendance(int AttendanceId, int? StudentId = null, int? SubjectId = null, string? Date = null)
        {
            DateOnly? parsedDate = null;

            if (Date != null)
            {
                if (!DateOnly.TryParseExact(Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tempParsedDate))
                {
                    return BadRequest("Formato de fecha no válido. Utilice el formato dd-MM-yyyy.");
                }
                parsedDate = tempParsedDate;
            }
            else
            {
                parsedDate = DateOnly.MinValue;
            }

            var updatedAttendance = await _attendanceService.UpdateAttendance(AttendanceId, StudentId, SubjectId, parsedDate);
            if (updatedAttendance == null)
            {
                return NotFound();
            }
            return Ok(updatedAttendance);
        }
         [HttpDelete("{AttendanceId}")]
         public async Task<ActionResult<Attendance>> DeleteAttendance(int AttendanceId, [FromQuery] bool AttendanceFlag)
          {
                var attendance = await _attendanceService.DeleteAttendance(AttendanceId, AttendanceFlag);
                if (attendance == null)
                {
                    return NotFound();
                }
                return Ok(attendance);
          }
  
    }
}
