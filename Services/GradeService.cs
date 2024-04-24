using StudentApplication.Models;
using StudentApplication.Repositories;

namespace StudentApplication.Services
{

    public interface IGradeService
    {
        Task<List<Grade>> GetGrades();
        Task<Grade> GetGrade(int GradeId);
        Task<Grade> CreateGrade(int StudentId, int SubjectId, string Description, float Score);
        Task<Grade> UpdateGrade(int GradeId, int? StudentId = null, int? SubjectId = null, float? Score = null, string? Description = null);
        Task<Grade> DeleteGrade(int GradeId, bool ActiveGrade = false);
    }

    public class GradeService : IGradeService
    {

        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<Grade> CreateGrade(int StudentId, int SubjectId, string Description, float Score)
        {
            return await _gradeRepository.CreateGrade(StudentId, SubjectId, Description, Score);
        }

        public async Task<Grade> GetGrade(int GradeId)
        {
            return await _gradeRepository.GetGrade(GradeId);
        }

        public async Task<List<Grade>> GetGrades()
        {
            return await _gradeRepository.GetGrades();
        }

        public async Task<Grade> UpdateGrade(int GradeId, int? StudentId = null, int? SubjectId = null, float? Score = null, string? Description = null)
        {
            Grade grade = await _gradeRepository.GetGrade(GradeId);

            if (GradeId <= 0)
            {
                throw new ArgumentException("El ID de la calificación debe ser un número positivo.");
            }

            if (grade == null)
            {
                return null;
            }

            if (StudentId != null)
            {
                grade.StudentId = StudentId.Value;
            }
            if (SubjectId != null)
            {
                grade.SubjectId = SubjectId.Value;
            }
            if (Score != null)
            {
                grade.Score = Score.Value;
            }
            if (Description != null)
            {
                grade.Description = Description;
            }

            return await _gradeRepository.UpdateGrade(grade);
        }



        public async Task<Grade> DeleteGrade(int GradeId, bool ActiveGrade)
        {
            Grade grade = await _gradeRepository.GetGrade(GradeId);
            if (grade == null)
            {
                return null;
            }
            if (ActiveGrade)
            {
                grade.ActiveGrade = ActiveGrade;
            }
            await _gradeRepository.DeleteGrade(GradeId,ActiveGrade);
            return grade;
        }

    }
}