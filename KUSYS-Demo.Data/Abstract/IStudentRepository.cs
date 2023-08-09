using System;
using KUSYS_Demo.Data.Utilities.Models;
using KUSYS_Demo.Entity;

namespace KUSYS_Demo.Data.Abstract
{
	public interface IStudentRepository : IGenericRepository<Student>
    {
        void AddStudent(StudentViewModel model);
        void UpdateStudent(StudentViewModel model);
        void DeleteStudent(int studentId);
    }
}

