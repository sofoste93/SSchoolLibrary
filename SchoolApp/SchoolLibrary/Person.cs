using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
	// add the keyword 'abstract'
	public abstract class Person 
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		// assign grade
		public abstract float ComputeGradeAVG();

		// send Message - Virtual Method
		public virtual string SendMessage(string message)
		{
			var sBuilder = new StringBuilder();
			var timeStamp = string.Format("Sent on {0:D} at {0:t}", DateTime.Now);
			sBuilder.AppendLine(timeStamp);
			sBuilder.AppendLine("");
			sBuilder.AppendLine("Dear " + FirstName + ",");
			sBuilder.AppendLine(message);

			return sBuilder.ToString();
		}

	}
}
