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

		public override float ComputeGradeAVG()
		{
			var gAvg = 4.0f;
			return gAvg;
			// throw new NotImplementedException();
		}

		// send Msg
		public override string SendMessage(string message)
		{
			// return base.SendMessage(message);
			var originalMsg = base.SendMessage(message);
			var sBuilder = new StringBuilder(originalMsg);
			sBuilder.AppendLine("This message is private and confidential.");

			return sBuilder.ToString();
		}
	}
}
