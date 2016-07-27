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
    [Activity(Label = "LoginRegister")]
    public class LoginRegisterScreen : Activity
    {
        Button backButton;
        Button loginButton;
        Button registerButton;
        EditText userName;
        EditText userPassword;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set our layout to be the home screen
            SetContentView(Resource.Layout.LoginRegister);
            userName = FindViewById<EditText>(Resource.Id.LoginUserName);
            userPassword = FindViewById<EditText>(Resource.Id.LoginUserPassword);
            backButton = FindViewById<Button>(Resource.Id.LoginBackButton);
            loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            registerButton = FindViewById<Button>(Resource.Id.RegisterButton);




            // button clicks 
            backButton.Click += (sender, e) => { Back(); };
            loginButton.Click += (sender, e) => { Login(); };
            registerButton.Click += (sender, e) => { Register(); };


        }


        void Login()
        {
            TaskyApp.Current.TaskUser = Task.Run(() => TaskyApp.Current.TodoContractClient
                                            .SetUser(userName.Text, userPassword.Text)).Result;
            Finish();
        }

        void Register()
        {
            TaskyApp.Current.TaskUser = Task.Run(() => TaskyApp.Current.TodoContractClient
                                            .RegisterUser(userName.Text, userPassword.Text)).Result;
            Finish();
        }


        void Back()
        {
            Finish();
        }
    }
}