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

		    AppDelegate.Current.TaskUser = Task.Run(() => AppDelegate.Current.TodoContractClient.SetUser("charlie", "test")).Result; 
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
            userDetailDialog = new UserDetailDialog();
            context = new BindingContext(this, userDetailDialog, "User Details");
            detailsScreen = new DialogViewController(context.Root, true);
            ActivateController(detailsScreen);
        }

        public async Task SaveTask()
		{

            context.Fetch (); // re-populates with updated values
			currentItem.Name = taskDialog.Name;
			currentItem.Notes = taskDialog.Notes;
		    currentItem.Reward = Int32.Parse(taskDialog.Reward);
			// TODO: show the completion status in the UI
			currentItem.Done = taskDialog.Done;

		    await AppDelegate.Current.TodoContractClient.SaveItem(currentItem);
			NavigationController.PopViewController (true);
		}
		public async Task DeleteTask ()
		{
            //if (currentItem.ID >= 0)
                //AppDelegate.Current.TodoManager.DeleteTask (currentItem.ID);
		    if (currentItem.ID != null)
		    {
		        await AppDelegate.Current.TodoContractClient.DeleteItem(currentItem.ID);

		    }

			NavigationController.PopViewController (true);
		}

	    public async Task Login()
	    {
	        
	    }

	    public async Task Register()
	    {
	        
	    }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			// reload/refresh
			PopulateTable();			
		}
		
		protected async Task PopulateTable()
		{
			tasks = await AppDelegate.Current.TodoContractClient.GetItems();
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
            await AppDelegate.Current.TodoContractClient.DeleteItem(tasks[rowId].ID);
		}
	}
}