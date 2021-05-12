using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// import referencies
using SchoolLibrary;

namespace SchoolFormApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnClickToTest_Click(object sender, EventArgs e)
		{
			var testSchool = new School();

			// working with user input
			testSchool.Name = textName.Text;
			testSchool.Address = textAddress.Text;
			testSchool.City = textCity.Text;
			testSchool.State = textState.Text;
			testSchool.Zip = textZip.Text;
			testSchool.PhoneNumber = textPhone.Text;

			// Handel exception for twitterAddress
			try
			{
				testSchool.TwitterAddress = textTwitter.Text;
			}
			catch (Exception except)
			{
				MessageBox.Show(except.Message); // remember: twitter address must begin with '@'
				//throw;
			}

			// test ToString
			MessageBox.Show(testSchool.ToString());

			// find out: if u can get access to student/teacher properties
			var student = new Student(); // working with subclasses
			
		}
	}
}
