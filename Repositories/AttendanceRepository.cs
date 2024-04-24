using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;


namespace StudentApplication.Repositories
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAttendances();
        Task<Attendance> GetAttendance(int AttendanceId);
        Task<Attendance> CreateAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag);
        Task<Attendance> UpdateAttendance(Attendance attendance);
        Task<Attendance> DeleteAttendance(int AttendanceId,bool AttendanceFlag);
    }

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly StudentDbContext _db;

        public AttendanceRepository(StudentDbContext db)
        {
            _db = db;
        }

        public async Task<Attendance> CreateAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag)
        {
            Attendance newAttendance = new Attendance
            {
                StudentId = StudentId,
                SubjectId = SubjectId,
                Date = Date,
                AttendanceFlag = AttendanceFlag
            };

            await _db.Attendances.AddAsync(newAttendance);
            await _db.SaveChangesAsync();
            return newAttendance;
        }

        public async Task<Attendance> DeleteAttendance(int AttendanceId, bool AttendanceFlag)
        {
            var Attendance = await _db.Attendances.FirstOrDefaultAsync(u => u.AttendanceId == AttendanceId);
            if (Attendance == null)
            {
                return null;
            }

            Attendance.AttendanceFlag = AttendanceFlag;
            await _db.SaveChangesAsync();
            return Attendance;
        }

        public async Task<List<Attendance>> GetAttendances()
        {
            return await _db.Attendances.ToListAsync();
        }

        public async Task<Attendance> GetAttendance(int AttendanceId)
        {
            return await _db.Attendances.FirstOrDefaultAsync(u => u.AttendanceId == AttendanceId);
        }

        public async Task<Attendance> UpdateAttendance(Attendance attendance)
        {
            _db.Attendances.Update(attendance);
            await _db.SaveChangesAsync();
            return attendance;
        }
    }
}
