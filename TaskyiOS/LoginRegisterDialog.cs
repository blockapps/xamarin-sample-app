using System;
using UIKit;
using Tasky.PortableLibrary;
using MonoTouch.Dialog;

namespace Tasky.ApplicationLayer 
{
	/// <summary>
	/// Wrapper class for Task, to use with MonoTouch.Dialog. If it was just iOS platform
	/// we could apply these attributes directly to the Task class, but because we share that
	/// with other platforms this wrapper provides a bridge to MonoTouch.Dialog.
	/// </summary>
	public class LoginRegisterDialog 
	{
		public LoginRegisterDialog()
		{
		        Name = "";
		        Password = "";
		}
		
		[Entry("username")]
		public string Name { get; set; }

        [Password("enter password")]
		public string Password { get; set; }


		[Section ("")]
		[OnTap ("Login")]
		[Alignment (UITextAlignment.Center)]
    	public string Login;
		
		[Section ("")]
		[OnTap ("Register")]
		[Alignment (UITextAlignment.Center)]
    	public string Register;
	}
}