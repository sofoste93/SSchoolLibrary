using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
	/*
	 * Utility classes are common and essentially,
	 * they are usually just collections of utility method
	 * usually static so let's call this ScoreUtility and this is going to have
	 * some utility methods in it for computing scores.
	 */
	public class ScoreUtility
	{
		// create a static method
		public static IScored BestOfTwo(IScored Assigment0, IScored Assigment1)
		{
			var score0 = Assigment0.Score / Assigment0.MaximumScore;
			var score1 = Assigment1.Score / Assigment1.MaximumScore;

			// using if-statement
			if(score0 > score1)
			{
				return Assigment0;
			} else
			{
				return Assigment1; 
			}
		}
	}
}
