using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KUSYS_Demo.Data.Abstract;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUSYS_Demo.WebUI.Controllers
{
    public class CourseController : Controller
    {
        IUnitOfWork unitOfWork;
        public CourseController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var courseList = unitOfWork.Courses.GetAll()
                .Select(s => new CourseListViewModel
                {
                    CourseId = s.CourseId,
                    CourseName = s.CourseName
                });

            return View(courseList);
        }

        [HttpGet]
        [Authorize(Policy ="AdminRolePolicy")]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult AddCourse(CourseListViewModel model)
        {
            if (ModelState.IsValid)
            {
                Data.Utilities.Models.CourseViewModel courseViewModel = new Data.Utilities.Models.CourseViewModel();
                courseViewModel.CourseName = model.CourseName;
                courseViewModel.CourseId = model.CourseId;
                unitOfWork.Courses.AddCourse(courseViewModel);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult UpdateCourse(string id)
        {
            var course = unitOfWork.Courses.GetAll().FirstOrDefault(x => x.CourseId == id);
            CourseListViewModel courseListViewModel = new CourseListViewModel();
            courseListViewModel.CourseId = course.CourseId;
            courseListViewModel.CourseName = course.CourseName;
            return View(courseListViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult UpdateCourse(CourseListViewModel model)
        {
            Data.Utilities.Models.CourseViewModel courseViewModel = new Data.Utilities.Models.CourseViewModel();
            courseViewModel.CourseId = model.CourseId;
            courseViewModel.CourseName = model.CourseName;
            unitOfWork.Courses.UpdateCourse(courseViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Policy = "AdminRolePolicy")]
        public IActionResult DeleteCourse(string id)
        {
            unitOfWork.Courses.DeleteCourse(id);

            return RedirectToAction("Index");
        }
    }
}

