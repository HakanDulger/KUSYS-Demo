using System;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Entity
{
	public class Matchings
	{
		[Key]
		public int MatchingId { get; set; }
		public string CourseId { get; set; }
		public int StudentId { get; set; }
	}
}

