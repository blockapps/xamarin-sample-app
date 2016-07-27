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
        private Button changeUserButton;
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
            changeUserButton = FindViewById<Button>(Resource.Id.ChangeUserButton);

            backButton.Click += (sender, e) => { Back(); };
            changeUserButton.Click += (sender, e) => { StartActivity(typeof(LoginRegisterScreen)); }; 

            if (TaskyApp.Current.TaskUser == null)
            {
                StartActivity(typeof(LoginRegisterScreen));
                return;
            }

            var user = TaskyApp.Current.TaskUser;
            var account = user.Accounts[user.DefaultAccount];

            userName.Text = user.Name;
            userAddress.Text = account != null ? account.Address : "";
            userBalance.Text = account != null ? (double.Parse(account.Balance)/ 1000000000000000000).ToString() : "";

        }

        protected override void OnResume()
        {
            base.OnResume();
            var task = Task.Run(() => TaskyApp.Current.TaskUser.Accounts[TaskyApp.Current.TaskUser.DefaultAccount].RefreshAccount());
            task.Wait();
            var user = TaskyApp.Current.TaskUser;
            var account = user.Accounts[user.DefaultAccount];

            userName.Text = user.Name;
            userAddress.Text = account != null ? account.Address : "";
            userBalance.Text = account != null ? (double.Parse(account.Balance) / 1000000000000000000).ToString() : "";
        }

        void Back()
        {
            Finish();
        }
    }
}