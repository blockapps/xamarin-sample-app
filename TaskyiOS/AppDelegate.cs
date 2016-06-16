using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using SQLite;
using Tasky.PortableLibrary;
using System.IO;
using TaskyPortableLibrary;

namespace Tasky 
{
	public class Application 
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}

	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate 
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navController;
		UITableViewController homeViewController;

		public static AppDelegate Current { get; private set; }
		public TodoItemManager TodoManager { get; set; }
        public TodoContractManager TodoContractMngr { get; set; }
		SQLiteConnection conn;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Current = this;

			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// make the window visible
			window.MakeKeyAndVisible ();


            TodoContractMngr = new TodoContractManager("http://40.118.255.235:8000",
                "http://40.118.255.235/eth/v1.2");

            // create our nav controller
            navController = new UINavigationController ();

			// create our Todo list screen
			homeViewController = new Screens.HomeScreen ();

			// push the view controller onto the nav controller and show the window
			navController.PushViewController(homeViewController, false);
			window.RootViewController = navController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}