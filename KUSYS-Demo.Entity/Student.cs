using System;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Entity
{
	public class Student
	{
        [Key]
        public int StudentId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
        [StringLength(11)]
        public string? PhoneNumber { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [StringLength(50)]
        public string? EMail { get; set; }
        [StringLength(25)]
        public string UserName { get; set; }
        [StringLength(25)]
        public string Password { get; set; }
        public Guid IdentityId { get; set; }
    }
}

