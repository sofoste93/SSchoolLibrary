using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
	public class Student : Person // now we can access Person
	{
		public enum GradeLevels { Freshman, Sophmore, Junior, Senior}
		public GradeLevels gradeLevels { get; set; }
	}
}
