using System;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Data.Utilities.Models;
using KUSYS_Demo.Entity;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Data.Concrete.EfCore
{
	public class EfStudentRepository : EfGenericRepository<Student>, IStudentRepository
    {
        public EfStudentRepository(DbContext _context) : base(_context)
        {
        }
        public KusysDbContext KusysDbContext
        {
            get { return context as KusysDbContext; }
        }

        public void AddStudent(StudentViewModel model)
        {
            try
            {
                Student student = new Student();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.BirthDate = model.BirthDate;
                student.PhoneNumber = model.PhoneNumber;
                student.Address = model.Address;
                student.EMail = model.EMail;
                student.UserName = model.UserName;
                student.Password = model.Password;
                student.IdentityId = model.IdentityId;

                KusysDbContext.Student.Add(student);
                KusysDbContext.SaveChanges();

                foreach (var item in model.Courses)
                {
                    Matchings matchings = new Matchings();
                    matchings.CourseId = item.CourseId;
                    matchings.StudentId = student.StudentId;
                    KusysDbContext.Matchings.Add(matchings);
                }

                KusysDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteStudent(int studentId)
        {
            try
            {
                var student = KusysDbContext.Student.FirstOrDefault(f => f.StudentId == studentId);
                if (student != null)
                {
                    var matchings = KusysDbContext.Matchings.Where(x => x.StudentId == student.StudentId).ToList();
                    foreach (var matching in matchings)
                    {
                        KusysDbContext.Matchings.Remove(matching);
                    }

                    KusysDbContext.SaveChanges();

                    KusysDbContext.Student.Remove(student);
                    KusysDbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Not Found!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStudent(StudentViewModel model)
        {
            try
            {
                var student = KusysDbContext.Student.FirstOrDefault(f => f.StudentId == model.StudentId);
                if (student != null)
                {
                    student.FirstName = model.FirstName;
                    student.LastName = model.LastName;
                    student.BirthDate = model.BirthDate;
                    student.PhoneNumber = model.PhoneNumber;
                    student.Address = model.Address;
                    student.EMail = model.EMail;

                    KusysDbContext.Student.Update(student);
                    KusysDbContext.SaveChanges();

                    foreach (var item in model.Courses)
                    {
                        Matchings matchings = new Matchings();
                        matchings.CourseId = item.CourseId;
                        matchings.StudentId = student.StudentId;
                        KusysDbContext.Matchings.Add(matchings);
                    }

                    KusysDbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Not Found!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

