using System;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.WebUI.Models
{
	public class StudentListViewModel
	{
		public int StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public List<CourseListViewModel> Courses { get; set; }
		public string? Course { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EMail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

