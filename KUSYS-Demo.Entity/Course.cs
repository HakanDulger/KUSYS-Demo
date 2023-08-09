using System;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Entity
{
	public class Course
	{
        [Key]
        public string CourseId { get; set; }
        [StringLength(75)]
        public string CourseName { get; set; }
    }
}

