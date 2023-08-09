using System;
using System.ComponentModel.DataAnnotations;
using KUSYS_Demo.Entity;

namespace KUSYS_Demo.Data.Utilities.Models
{
	public class StudentViewModel
	{
		public int StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public List<CourseViewModel> Courses { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EMail { get; set; }
    }
}

