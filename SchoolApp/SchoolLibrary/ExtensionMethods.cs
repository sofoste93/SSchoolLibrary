using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolLibrary
{
	// extension method must be defined in a non-generic static class
	public static class ExtensionMethods // update adding keyword 'static
	{
		// static method
		public static int WordCount(this string myString)
		{
			var wordCount = myString.Split(new char[] { ' ', '.', '?' },
				StringSplitOptions.RemoveEmptyEntries).Length;

			return wordCount;
		}
	}
}
