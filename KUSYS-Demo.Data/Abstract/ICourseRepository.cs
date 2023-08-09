using System;
using KUSYS_Demo.Data.Utilities.Models;
using KUSYS_Demo.Entity;

namespace KUSYS_Demo.Data.Abstract
{
	public interface ICourseRepository : IGenericRepository<Course>
    {
        void AddCourse(CourseViewModel model);
        void UpdateCourse(CourseViewModel model);
        void DeleteCourse(string courseId);
    }
}

