using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.Data.Utilities.Models;
using KUSYS_Demo.Entity;
using KUSYS_Demo.Identity;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUSYS_Demo.WebUI.Controllers
{
    public class StudentController : Controller
    {
        IUnitOfWork unitOfWork;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public StudentController(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<StudentListViewModel> studentList = new List<StudentListViewModel>();
            if (User.IsInRole("Admin"))
            {
                studentList = unitOfWork.Students.GetAll()
                .Select(s => new StudentListViewModel
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthDate = s.BirthDate,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    EMail = s.EMail
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
            }
            else
            {

                studentList = unitOfWork.Students.GetAll()
                .Select(s => new StudentListViewModel
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthDate = s.BirthDate,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    EMail = s.EMail
                }).Where(x => x.UserName == ).ToList();
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
            }
            
            return View(studentList);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            StudentListViewModel model = new StudentListViewModel();
            var courses = unitOfWork.Courses.GetAll().Select(s => new CourseListViewModel
            {
                CourseId = s.CourseId,
                CourseName = s.CourseName
            }).ToList();
            model.Courses = new List<CourseListViewModel>();
            model.Courses.AddRange(courses);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddStudent(StudentListViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.EMail,
                    Name = model.FirstName + " " + model.LastName,
                    IsActive = true,
                    PhoneNumber = model.PhoneNumber,
                    IdentityId = Guid.NewGuid()
                };

                var result = userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    var roles = userManager.GetRolesAsync(user).GetAwaiter().GetResult();
                    List<string> role = new List<string>();
                    role.Add("User");

                    result = userManager.AddToRolesAsync(user, role).GetAwaiter().GetResult();

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Seçilen roller kullanıcıya eklenemiyor.");
                        return View(model);
                    }

                    Data.Utilities.Models.StudentViewModel studentViewModel = new Data.Utilities.Models.StudentViewModel();
                    studentViewModel.FirstName = model.FirstName;
                    studentViewModel.LastName = model.LastName;
                    studentViewModel.BirthDate = model.BirthDate;
                    studentViewModel.PhoneNumber = model.PhoneNumber;
                    studentViewModel.Address = model.Address;
                    studentViewModel.EMail = model.EMail;
                    studentViewModel.UserName = model.UserName;
                    studentViewModel.Password = model.Password;
                    studentViewModel.IdentityId = user.IdentityId;

                    studentViewModel.Courses = new List<Data.Utilities.Models.CourseViewModel>();
                    foreach (var course in model.Courses)
                    {
                        Data.Utilities.Models.CourseViewModel courseViewModel = new Data.Utilities.Models.CourseViewModel();
                        if (course.IsSelected)
                        {
                            courseViewModel.CourseId = course.CourseId;
                            courseViewModel.CourseName = course.CourseName;
                            studentViewModel.Courses.Add(courseViewModel);
                        }
                    }


                    unitOfWork.Students.AddStudent(studentViewModel);
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                var courses = unitOfWork.Courses.GetAll().Select(s => new CourseListViewModel
                {
                    CourseId = s.CourseId,
                    CourseName = s.CourseName
                }).ToList();
                model.Courses = new List<CourseListViewModel>();
                model.Courses.AddRange(courses);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult UpdateStudent(int id)
        {
            var student = unitOfWork.Students.Get(id);
            StudentListViewModel studentListViewModel = new StudentListViewModel();
            studentListViewModel.StudentId = student.StudentId;
            studentListViewModel.FirstName = student.FirstName;
            studentListViewModel.LastName = student.LastName;
            studentListViewModel.BirthDate = student.BirthDate;
            studentListViewModel.PhoneNumber = student.PhoneNumber;
            studentListViewModel.Address = student.Address;
            studentListViewModel.EMail = student.EMail;

            var mathings = unitOfWork.Matchings.GetAll().Where(x => x.StudentId == student.StudentId).ToList();
            var courses = mathings.Select(s => s.CourseId).ToList();
            studentListViewModel.Courses = new List<CourseListViewModel>();
            if (mathings != null && mathings.Count() > 0)
            {
                var courseList = unitOfWork.Courses.GetAll().Where(x => !courses.Contains(x.CourseId)).ToList();
                if (courseList != null && courseList.Count() > 0)
                {
                    foreach (var item in courseList)
                    {
                        CourseListViewModel courseViewModel = new CourseListViewModel();
                        courseViewModel.CourseId = item.CourseId;
                        courseViewModel.CourseName = item.CourseName;
                        studentListViewModel.Courses.Add(courseViewModel);
                    }
                }
            }
            else
            {
                var courseslist = unitOfWork.Courses.GetAll().Select(s => new CourseListViewModel
                {
                    CourseId = s.CourseId,
                    CourseName = s.CourseName
                }).ToList();

                studentListViewModel.Courses.AddRange(courseslist);
            }
            return View(studentListViewModel);
        }

        [HttpPost]
        public IActionResult UpdateStudent(StudentListViewModel model)
        {
            Data.Utilities.Models.StudentViewModel studentViewModel = new Data.Utilities.Models.StudentViewModel();
            studentViewModel.StudentId = model.StudentId;
            studentViewModel.FirstName = model.FirstName;
            studentViewModel.LastName = model.LastName;
            studentViewModel.BirthDate = model.BirthDate;
            studentViewModel.PhoneNumber = model.PhoneNumber;
            studentViewModel.Address = model.Address;
            studentViewModel.EMail = model.EMail;

            studentViewModel.Courses = new List<Data.Utilities.Models.CourseViewModel>();
            foreach (var course in model.Courses)
            {
                Data.Utilities.Models.CourseViewModel courseViewModel = new Data.Utilities.Models.CourseViewModel();
                if (course.IsSelected)
                {
                    courseViewModel.CourseId = course.CourseId;
                    courseViewModel.CourseName = course.CourseName;
                    studentViewModel.Courses.Add(courseViewModel);
                }
            }

            unitOfWork.Students.UpdateStudent(studentViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize("AdminRolePolicy")]
        public IActionResult DeleteStudent(int id)
        {
            unitOfWork.Students.DeleteStudent(id);

            return RedirectToAction("Index");
        }
    }
}

