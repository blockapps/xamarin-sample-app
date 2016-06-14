using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskyPortableLibrary;
using BlockAppsSDK.Users;

namespace TodoContractManagerTest.cs
{
    [TestClass]
    public class TodoContractManagerTest
    {
        private TodoContractManager TodoContractMngr { get; set; }
        [TestMethod]
        public void GetItemsTest()
        {

        }

        [TestInitialize]
        public async Task SetupTests()
        {
            this.TodoContractMngr = new TodoContractManager(
                "http://chartest.centralus.cloudapp.azure.com:8000/", 
                "http://charlietest.centralus.cloudapp.azure.com/strato-single/eth/v1.2/");

            var testUser = 
        }
    }
}
