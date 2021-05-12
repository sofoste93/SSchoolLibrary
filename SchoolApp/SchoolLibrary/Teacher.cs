using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
	public class Teacher : Person  // now we can access Person 
	{

		public string Subject { get; set; }

		public override float ComputeGradeAVG()
		{
			var tGp = 0.0f;
			// throw new NotImplementedException();
			return tGp;
		}
	}
}
