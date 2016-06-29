using System;
using UIKit;
using Tasky.PortableLibrary;
using MonoTouch.Dialog;
using BlockAppsSDK.Users;

namespace Tasky.ApplicationLayer 
{
	/// <summary>
	/// Wrapper class for Task, to use with MonoTouch.Dialog. If it was just iOS platform
	/// we could apply these attributes directly to the Task class, but because we share that
	/// with other platforms this wrapper provides a bridge to MonoTouch.Dialog.
	/// </summary>
	public class UserDetailDialog 
	{
		public UserDetailDialog()
		{
		    var user = AppDelegate.Current.TaskUser;
			Name = user.Name;
			Ether = (double.Parse(user.Accounts[user.DefaultAccount].Balance) / 1000000000000000000).ToString();
		    Address = user.Accounts[user.DefaultAccount].Address;
		}
		
		[Caption("")]
		public string Name { get; set; }

		[Caption("Ether Available: ")]
		public string Ether{ get; set; }

		[Caption("User Address is: ")]
		public string Address{ get; set; }

	}
}