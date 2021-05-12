using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
	/**
	 * Preparing for interfaces
	 */

	public class ScienceExperiment : IScored  // accessing the interface
	{
		public string Hypothesis { get; set; }
		public string Materials { get; set; }
		public string Method { get; set; }
		public string Conclusion { get; set; }
		
		// implement interface
		public float Score 
		{
			get; set;
			// get => throw new NotImplementedException();
			// set => throw new NotImplementedException();
		}
		public float MaximumScore {
			get; set;
			// get => throw new NotImplementedException(); set => throw new NotImplementedException();
		}
	}
}
