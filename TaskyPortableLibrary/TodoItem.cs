using System;
using SQLite;

namespace Tasky.PortableLibrary 
{
	/// <summary>
	/// Todo Item business object
	/// </summary>
	public class TodoItem 
	{
		public TodoItem ()
		{
		}

		// SQLite attributes
		[PrimaryKey, AutoIncrement]

        public string ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }	// TODO: add this field to the user-interface
        public int Reward { get; set; } // This is in ether not wei
	}
}