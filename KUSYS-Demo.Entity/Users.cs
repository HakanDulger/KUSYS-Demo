using System;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Entity
{
	public class Users
	{
        [Key]
        public int UserId { get; set; }
		public Guid IdentityId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
        [StringLength(25)]
        public string UserName { get; set; }
        [StringLength(25)]
        public string Password { get; set; }
        [StringLength(50)]
        public string EMail { get; set; }

    }
}

