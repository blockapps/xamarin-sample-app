using System;
using System.IO;
using SQLite;
using Android.App;
using Tasky.PortableLibrary;
using TaskyPortableLibrary;

namespace TaskyAndroid
{
	[Application]
	public class TaskyApp : Application {
		public static TaskyApp Current { get; private set; }

		public TodoItemManager TodoManager { get; set; }
        public TodoContractManager TodoContractMngr { get; set; }
		SQLiteConnection conn;

		public TaskyApp(IntPtr handle, global::Android.Runtime.JniHandleOwnership transfer)
			: base(handle, transfer) {
			Current = this;
		}

		public override void OnCreate()
		{
			base.OnCreate();

            //TodoContractMngr = new TodoContractManager("http://xamarin.centralus.cloudapp.azure.com:8000",
            //    "http://xamarin.centralus.cloudapp.azure.com/eth/v1.2");
            TodoContractMngr = new TodoContractManager("http://40.117.237.163:8000",
                "http://40.117.237.163/eth/v1.2");
        }
	}
}

