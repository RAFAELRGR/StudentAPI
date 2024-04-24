using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentApplication.Models;
using StudentApplication.Repositories;

namespace StudentApplication.Services
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetSubjects();
        Task<Subject> GetSubject(int SubjectId);
        Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode, int TeacherId);
        Task<Subject> UpdateSubject(int SubjectId, int? ClassId = null, string? SubjectName = null, string? Subjectcode = null, int? TeacherId = null);
        Task<Subject> DeleteSubject(int SubjectId, bool ActiveSubject = false);
    }

    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Subject> CreateSubject(int ClassId, string SubjectName, string Subjectcode, int TeacherId)
        {
            return await _subjectRepository.CreateSubject(ClassId, SubjectName, Subjectcode, TeacherId);
        }

        public async Task<Subject> GetSubject(int SubjectId)
        {
            return await _subjectRepository.GetSubject(SubjectId);
        }

        public async Task<List<Subject>> GetSubjects()
        {
            return await _subjectRepository.GetSubjects();
        }

        public async Task<Subject> UpdateSubject(int SubjectId, int? ClassId = null, string? SubjectName = null, string? Subjectcode = null, int? TeacherId = null)
        {
            Subject subject = await _subjectRepository.GetSubject(SubjectId);

            if (subject == null)
            {
                return null;
            }

            if (ClassId != null)
            {
                subject.ClassId = ClassId.Value;
            }

            if (SubjectName != null)
            {
                subject.SubjectName = SubjectName;
            }

            if (Subjectcode != null)
            {
                subject.Subjectcode = Subjectcode;
            }

            if (TeacherId != null)
            {
                subject.TeacherId = TeacherId.Value;
            }

            return await _subjectRepository.UpdateSubject(subject);
        }

        public async Task<Subject> DeleteSubject(int SubjectId, bool ActiveSubject = false)
        {
            Subject subject = await _subjectRepository.GetSubject(SubjectId);

            if (subject == null)
            {
                return null;
            }

            if (ActiveSubject)
            {
                subject.ActiveSubject = ActiveSubject;
            }

            await _subjectRepository.DeleteSubject(SubjectId, ActiveSubject);
            return subject;
        }

    }
}
