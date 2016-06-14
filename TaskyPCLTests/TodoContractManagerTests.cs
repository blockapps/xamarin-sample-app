using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockAppsSDK;
using BlockAppsSDK.Users;
using BlockAppsSDK.Contracts;
using Tasky.PortableLibrary;
using TaskyPortableLibrary;

namespace TaskyPCLTests
{
    [TestClass]
    public class TodoContractManagerTests
    {
        [TestMethod]
        public async Task GetItemsTest()
        {
            var todoContractMgr = new TodoContractManager("http://localhost:8000",
                "http://strato-dev4.blockapps.net/eth/v1.2");

            var items = await todoContractMgr.GetItems();
            var x = "sup";
        }

        [TestMethod]
        public async Task GetItemTest()
        {
            var todoContractMgr = new TodoContractManager("http://localhost:8000",
                "http://strato-dev4.blockapps.net/eth/v1.2");

            var items = await todoContractMgr.GetItem("4009e23d31d50609b5ab1b7da6e8aa6720ae8215");
            var x = "sup";
        }

        [TestMethod]
        public async Task CreateItemTest()
        {
            var todoContractMgr = new TodoContractManager("http://localhost:8000",
                "http://strato-dev4.blockapps.net/eth/v1.2");
            var user = await User.GetUser("charlie", "test");


            var todo = new TodoItem
            {
                Done = false,
                Name = "Paint wall",
                Notes = "paint red",
                Reward = 2,
                //ID = "4009e23d31d50609b5ab1b7da6e8aa6720ae8215"
            };

            var items = await todoContractMgr.SaveItem(todo, user, user.Accounts[0]);
            var x = "sup";
        }

        [TestMethod]
        public async Task DeleteItemTest()
        {
            //468975202a2a2af8f6594e2a9ec76985177599b8
            var todoContractMgr = new TodoContractManager("http://localhost:8000",
                "http://strato-dev4.blockapps.net/eth/v1.2");
            var user = await User.GetUser("charlie", "test");
            var msg = await todoContractMgr.DeleteItem("468975202a2a2af8f6594e2a9ec76985177599b8", user, user.Accounts.FirstOrDefault());
            var x = "sup";
        }
    }
}
