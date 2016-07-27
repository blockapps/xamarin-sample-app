using System;
using System.Collections.Generic;
using System.Text;
using MonoTouch.Dialog;
using Tasky.ApplicationLayer;

namespace Tasky
{
    public class UserDetailsScreen : DialogViewController
    {
        private BindingContext Context;
	    private UserDetailDialog userDetailDialog;
        private LoginRegisterDialog loginRegisterDialog;
	    private DialogViewController loginRegisterScreen;
        public UserDetailsScreen(BindingContext context, bool pushing) : base(context.Root, pushing)
        {
            Context = context;
            userDetailDialog = new UserDetailDialog();
            context = new BindingContext(this, userDetailDialog, "User Details");
        }

        public void ChangeUser()
        {
            ShowLoginRegister();
        }

        protected void ShowLoginRegister()
        {
            loginRegisterDialog = new LoginRegisterDialog();
            Context = new BindingContext(this, loginRegisterDialog, "Login Register");
            loginRegisterScreen = new DialogViewController(Context.Root, true);
            ActivateController(loginRegisterScreen);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Context.Fetch();
        }
    }
}
