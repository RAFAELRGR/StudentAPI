using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApplication.Repositories
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetSubjects();
        Task<Subject> GetSubject(int SubjectId);
        Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode, int TeacherId);
        Task<Subject> UpdateSubject(Subject subject);
        Task<Subject> DeleteSubject(int SubjectId, bool ActiveSubject);
    }

    public class SubjectRepository : ISubjectRepository
    {
        private readonly StudentDbContext _db;

        public SubjectRepository(StudentDbContext db)
        {
            _db = db;
        }

        public async Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode, int TeacherId)
        {
            Subject newSubject = new Subject
            {
                ClassId = ClassId,
                SubjectName = SubjectName,
                Subjectcode = Subjectcode,
                TeacherId = TeacherId,
                ActiveSubject = true
            };

            await _db.Subjects.AddAsync(newSubject);
            await _db.SaveChangesAsync();
            return newSubject;
        }

        public async Task<Subject> DeleteSubject(int SubjectId, bool ActiveSubject)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(u => u.SubjectId == SubjectId);
            if (subject == null)
            {
                return null;
            }

            subject.ActiveSubject = ActiveSubject;
            await _db.SaveChangesAsync();
            return subject;
        }


        public async Task<List<Subject>> GetSubjects()
        {
            return await _db.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubject(int SubjectId)
        {
            return await _db.Subjects.FirstOrDefaultAsync(u => u.SubjectId == SubjectId);
        }

        public async Task<Subject> UpdateSubject(Subject subject)
        {
            _db.Subjects.Update(subject);
            await _db.SaveChangesAsync();
            return subject;
        }
    }
}
