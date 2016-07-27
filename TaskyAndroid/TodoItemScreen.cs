using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Tasky.PortableLibrary;
using TaskyAndroid;
using BlockAppsSDK.Users;

namespace TaskyAndroid.Screens
{
    /// <summary>
    /// View/edit a Task
    /// </summary>
    [Activity(Label = "TaskDetailsScreen")]
    public class TodoItemScreen : Activity
    {
        TodoItem task = new TodoItem();
        Button cancelDeleteButton;
        EditText notesTextEdit;
        EditText nameTextEdit;
        EditText rewardTextEdit;
        Button saveButton;
        CheckBox doneCheckbox;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            string taskID = Intent.GetStringExtra("TaskID");
            if (taskID != null)
            {
                //task = TaskyApp.Current.TodoManager.GetTask(taskID);
                Task<TodoItem> asyncTask = Task.Run(() => TaskyApp.Current.TodoContractClient.GetItem(taskID));
                task = asyncTask.Result;
            }

            // set our layout to be the home screen
            SetContentView(Resource.Layout.TaskDetails);
            nameTextEdit = FindViewById<EditText>(Resource.Id.NameText);
            notesTextEdit = FindViewById<EditText>(Resource.Id.NotesText);
            saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            rewardTextEdit = FindViewById<EditText>(Resource.Id.RewardText);

            // TODO: find the Checkbox control and set the value
            doneCheckbox = FindViewById<CheckBox>(Resource.Id.chkDone);
            doneCheckbox.Checked = task.Done;

            // find all our controls
            cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);

            // set the cancel delete based on whether or not it's an existing task
            cancelDeleteButton.Text = (task.ID == null ? "Cancel" : "Delete");

            nameTextEdit.Text = task.Name;
            notesTextEdit.Text = task.Notes;
            rewardTextEdit.Text = task.Reward.ToString();

            // button clicks 
            cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
            saveButton.Click += (sender, e) => { Save(); };
        }

        async Task Save()
        {
            task.Name = nameTextEdit.Text;
            task.Notes = notesTextEdit.Text;
            task.Reward = int.Parse(rewardTextEdit.Text);
            //TODO: 
            task.Done = doneCheckbox.Checked;

            //TaskyApp.Current.TodoManager.SaveTask(task);
            await TaskyApp.Current.TodoContractClient.SaveItem(task);

            Finish();
        }

        async Task CancelDelete()
        {
            if (task.ID != null)
            {
                //TaskyApp.Current.TodoManager.DeleteTask(task.ID);
                await TaskyApp.Current.TodoContractClient.DeleteItem(task.ID);
            }
            Finish();
        }
    }
}