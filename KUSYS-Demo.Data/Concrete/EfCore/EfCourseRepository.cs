using System;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Data.Utilities.Models;
using KUSYS_Demo.Entity;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.Data.Concrete.EfCore
{
	public class EfCourseRepository : EfGenericRepository<Course>, ICourseRepository
    {
        public EfCourseRepository(DbContext _context) : base(_context)
        {
        }
        public KusysDbContext KusysDbContext
        {
            get { return context as KusysDbContext; }
        }

        public void AddCourse(CourseViewModel model)
        {
            try
            {
                Course course = new Course();
                course.CourseId = model.CourseId;
                course.CourseName = model.CourseName;
                
                KusysDbContext.Course.Add(course);
                KusysDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void DeleteCourse(string courseId)
        {
            try
            {
                var course = KusysDbContext.Course.FirstOrDefault(f => f.CourseId == courseId);
                if (course != null)
                {
                    KusysDbContext.Course.Remove(course);
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

        public void UpdateCourse(CourseViewModel model)
        {
            try
            {
                var course = KusysDbContext.Course.FirstOrDefault(f => f.CourseId == model.CourseId);
                if (course != null)
                {
                    course.CourseName = model.CourseName;
                    KusysDbContext.Course.Update(course);
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

