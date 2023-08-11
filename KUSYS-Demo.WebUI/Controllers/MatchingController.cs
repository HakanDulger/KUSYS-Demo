using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Identity;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUSYS_Demo.WebUI.Controllers
{
    /// <summary>
    /// Only the admin role can
    /// </summary>
    [Authorize(Policy = "AdminRolePolicy")]
    public class MatchingController : Controller
    {
        IUnitOfWork unitOfWork;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;
        public MatchingController(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        /// <summary>
        /// List of student and course matchings
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<StudentListViewModel> studentList = new List<StudentListViewModel>();
            studentList = unitOfWork.Students.GetAll()
                .Select(s => new StudentListViewModel
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthDate = s.BirthDate,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    EMail = s.EMail,
                    UserName = s.UserName
                }).ToList();
            foreach (var item in studentList)
            {
                var matchings = unitOfWork.Matchings.GetAll().Where(x => x.StudentId == item.StudentId).ToList();
                item.Courses = new List<CourseListViewModel>();
                foreach (var matching in matchings)
                {
                    var course = unitOfWork.Courses.GetAll().FirstOrDefault(f => f.CourseId == matching.CourseId);
                    CourseListViewModel model = new CourseListViewModel();
                    model.CourseId = course.CourseId;
                    model.CourseName = course.CourseName;
                    item.Courses.Add(model);
                }
                item.Course = String.Join(",", item.Courses.Select(s => s.CourseName));
            }

            return View(studentList);
        }
    }
}

