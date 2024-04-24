using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;
using StudentApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApplication.Services
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAttendances();
        Task<Attendance> GetAttendance(int AttendanceId);
        Task<Attendance> CreateAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag);
        Task<Attendance> UpdateAttendance(int AttendanceId, int? StudentId = null, int? SubjectId = null, DateOnly? Date = null);
        Task<Attendance> DeleteAttendance(int AttendanceId, bool AttendanceFlag);
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<Attendance> CreateAttendance(int StudentId, int SubjectId, DateOnly Date, bool AttendanceFlag)
        {
            return await _attendanceRepository.CreateAttendance(StudentId, SubjectId, Date, AttendanceFlag);
        }

        public async Task<Attendance> DeleteAttendance(int AttendanceId, bool AttendanceFlag = false)
        {
            Attendance attendance = await _attendanceRepository.GetAttendance(AttendanceId);
            if (attendance == null)
            {
                return null;
            }
            attendance.AttendanceFlag = AttendanceFlag;
            await _attendanceRepository.DeleteAttendance(AttendanceId, AttendanceFlag);
            return attendance;
        }

        public async Task<Attendance> GetAttendance(int AttendanceId)
        {
            return await _attendanceRepository.GetAttendance(AttendanceId);
        }

        public async Task<List<Attendance>> GetAttendances()
        {
            return await _attendanceRepository.GetAttendances();
        }

        public async Task<Attendance> UpdateAttendance(int AttendanceId, int? StudentId = null, int? SubjectId = null, DateOnly? Date = null)
        {
            Attendance attendance = await _attendanceRepository.GetAttendance(AttendanceId);

            if (AttendanceId <= 0)
            {
                throw new ArgumentException("El ID de la asistencia debe ser un número positivo.");
            }

            if (attendance == null)
            {
                return null;
            }

            if (StudentId != null)
            {
                attendance.StudentId = StudentId.Value;
            }
            if (SubjectId != null)
            {
                attendance.SubjectId = SubjectId.Value;
            }
            if (Date != null)
            {
                attendance.Date = Date.Value;
            }

            return await _attendanceRepository.UpdateAttendance(attendance);
        }
    }
}
