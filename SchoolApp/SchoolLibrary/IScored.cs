using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
	// we only define the structure here
	public interface IScored
	{
		float Score { get; set; }
		float MaximumScore { get; set; }
	}
}
