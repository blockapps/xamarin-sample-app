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

			var sqliteFilename = "TodoItemDB.db3";
			string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(libraryPath, sqliteFilename);
			conn = new SQLiteConnection(path);

			//TodoManager = new TodoItemManager(conn);
            TodoContractMngr = new TodoContractManager("http://40.117.237.163:8000",
                "http://strato-dev4.blockapps.net/eth/v1.2");

		}
	}
}

