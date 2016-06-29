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
    [Activity(Label = "UserDetailScreen")]
    public class UserDetailScreen : Activity
    {
        Button backButton;
        TextView userName;
        TextView userAddress;
        TextView userBalance;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set our layout to be the home screen
            SetContentView(Resource.Layout.UserDetails);
            userName = FindViewById<TextView>(Resource.Id.UserNameText);
            userAddress = FindViewById<TextView>(Resource.Id.UserAddressText);
            userBalance = FindViewById<TextView>(Resource.Id.UserBalanceText);
            backButton = FindViewById<Button>(Resource.Id.BackButton);

            if (TaskyApp.Current.TaskUser == null)
            {
                TaskyApp.Current.TaskUser = Task.Run(() => TaskyApp.Current.TodoContractClient.SetUser("charlie", "test")).Result;
            }

            var user = TaskyApp.Current.TaskUser;
            var account = user.Accounts[user.DefaultAccount];

            userName.Text = user.Name;
            userAddress.Text = account != null ? account.Address : "";
            userBalance.Text = account != null ? (double.Parse(account.Balance)/ 1000000000000000000).ToString() : "";


            // button clicks 
            backButton.Click += (sender, e) => { Back(); };
        }


        void Back()
        {
            Finish();
        }
    }
}