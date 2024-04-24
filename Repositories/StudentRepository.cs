using Microsoft.EntityFrameworkCore;
using StudentApplication.Context;
using StudentApplication.Models;

namespace StudentApplication.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> createStudent(string StudentName, string StudentLastName, string Email, DateOnly Birthdate);
        Task<List<Student>> getStudents();
        Task<Student> getStudent(int StudentId);
        Task<Student> updateStudent(Student student);
        Task<Student> deleteStudent(int StudentId, bool Active);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _db;

        public StudentRepository(StudentDbContext db)
        {
            _db = db;
        }

        public async Task<Student> createStudent(string StudentName, string StudentLastName, string Email, DateOnly Birthdate)
        {
            Student newStudent = new Student
            {
                StudentName = StudentName,
                StudentLastName = StudentLastName,
                Email = Email,
                Birthdate = Birthdate,
                Active = true
            };

            await _db.Students.AddAsync(newStudent);
            await _db.SaveChangesAsync();
            return newStudent;
        }

        public async Task<List<Student>> getStudents()
        {
            return await _db.Students.ToListAsync();
        }

        public async Task<Student> getStudent(int StudentId)
        {
            return await _db.Students.FirstOrDefaultAsync(u => u.StudentId == StudentId);
        }

        public async Task<Student> updateStudent(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task<Student> deleteStudent(int StudentId, bool Active)
        {
            var student = await _db.Students.FirstOrDefaultAsync(u => u.StudentId == StudentId);
            if (student == null)
            {
                return null;
            }

            student.Active = Active;
            await _db.SaveChangesAsync();
            return student;
        }
    }

}