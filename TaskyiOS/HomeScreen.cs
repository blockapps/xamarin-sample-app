using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVFoundation;
using BlockAppsSDK.Users;
using CoreGraphics;
using UIKit;
using MonoTouch.Dialog;
using Tasky.PortableLibrary;
using Tasky.ApplicationLayer;

namespace Tasky.Screens {

	/// <summary>
	/// A UITableViewController that uses MonoTouch.Dialog - displays the list of Tasks
	/// </summary>
	public class HomeScreen : DialogViewController {
		// 
		List<TodoItem> tasks;
		
		// MonoTouch.Dialog individual TaskDetails view (uses /ApplicationLayer/TaskDialog.cs wrapper class)
		BindingContext context;
		TodoItemDialog taskDialog;
	    private UserDetailDialog userDetailDialog;
		TodoItem currentItem;
		DialogViewController detailsScreen;

		public HomeScreen () : base (UITableViewStyle.Plain, null)
		{
			Initialize();
		}
		
		protected void Initialize()
		{
			NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowTaskDetails(new TodoItem()); };
		    var image = UIImage.FromFile("gear.png");
		    var size = new CGSize(image.Size.Width/2.5, image.Size.Height / 2.5);
		    var scaleImaged = image.Scale(size);
            
            var settingsButton = new UIBarButtonItem(scaleImaged, UIBarButtonItemStyle.Plain,
               (sender, e) => { ShowUserDetails(); });
            NavigationItem.SetLeftBarButtonItem(settingsButton, false);
		}
		
		protected void ShowTaskDetails(TodoItem item)
		{
			currentItem = item;
			taskDialog = new TodoItemDialog (currentItem);
			context = new BindingContext (this, taskDialog, "Task Details");
			detailsScreen = new DialogViewController (context.Root, true);
			ActivateController(detailsScreen);
		}

        protected void ShowUserDetails()
        {
            Task<User> userTask = Task.Run(() => User.GetUser("charlie", "test").Result);
            var user = userTask.Result;
            userDetailDialog = new UserDetailDialog(user, 0);
            context = new BindingContext(this, userDetailDialog, "User Details");
            detailsScreen = new DialogViewController(context.Root, true);
            ActivateController(detailsScreen);
        }

        public async Task SaveTask()
		{
		    var user = await User.GetUser("charlie", "test");

            context.Fetch (); // re-populates with updated values
			currentItem.Name = taskDialog.Name;
			currentItem.Notes = taskDialog.Notes;
		    currentItem.Reward = Int32.Parse(taskDialog.Reward);
			// TODO: show the completion status in the UI
			currentItem.Done = taskDialog.Done;
			//AppDelegate.Current.TodoManager.SaveTask(currentItem);

		    await AppDelegate.Current.TodoContractMngr.SaveItem(currentItem, user, user.Accounts.FirstOrDefault());
			NavigationController.PopViewController (true);
		}
		public async Task DeleteTask ()
		{
            //if (currentItem.ID >= 0)
                //AppDelegate.Current.TodoManager.DeleteTask (currentItem.ID);
		    if (currentItem.ID != null)
		    {
                var user = await User.GetUser("charlie", "test");
		        await AppDelegate.Current.TodoContractMngr.DeleteItem(currentItem.ID, user, user.Accounts.FirstOrDefault());

		    }

			NavigationController.PopViewController (true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			// reload/refresh
			PopulateTable();			
		}
		
		protected async Task PopulateTable()
		{
			tasks = await AppDelegate.Current.TodoContractMngr.GetItems();
			// TODO: use this element, which displays a 'tick' when item is completed
			var rows = from t in tasks
				select (Element)new CheckboxElement ((t.Name == "" ? "<new task>" : t.Name), t.Done);
			var s = new Section ();
			s.AddAll(rows);
			Root = new RootElement("Tasky") {s}; 
		}
		public override void Selected (Foundation.NSIndexPath indexPath)
		{
			var todoItem = tasks[indexPath.Row];
			ShowTaskDetails(todoItem);
		}
		public override Source CreateSizingSource (bool unevenRows)
		{
			return new EditingSource (this);
		}
		public async Task DeleteTaskRow(int rowId)
		{
			//AppDelegate.Current.TodoManager.DeleteTask(tasks[rowId].ID);
            var user = await User.GetUser("charlie", "test");
            await AppDelegate.Current.TodoContractMngr.DeleteItem(tasks[rowId].ID, user, user.Accounts.FirstOrDefault());
		}
	}
}