using System;
using System.IO;
using SQLite;
using Android.App;
using BlockAppsSDK.Users;
using Tasky.PortableLibrary;
using TaskyPortableLibrary;

namespace TaskyAndroid
{
	[Application]
	public class TaskyApp : Application {
		public static TaskyApp Current { get; private set; }

        public TodoContractClient TodoContractClient { get; set; }
        public User TaskUser { get; set; }

		public TaskyApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
			: base(handle, transfer) {
			Current = this;
		}

		public override void OnCreate()
		{
			base.OnCreate();

            TodoContractClient = new TodoContractClient("http://40.118.255.235:8000",
                "http://40.118.255.235/eth/v1.2");
		}
	}
}

