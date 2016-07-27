using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockAppsSDK;
using BlockAppsSDK.Contracts;
using BlockAppsSDK.Users;

using Tasky.PortableLibrary;

namespace TaskyPortableLibrary
{
    public class TodoContractClient
    {
        private readonly string TaskContract = "contract Task { /** * It is helpful to think of * smart contracts as state machine. * In this example: * State 1: Deploy new smart task contract * State 2: Set task name and reward * State 3: Task Completed * State 4: Task Deleted */ address owner; address completedBy; uint taskReward; string stateMessage; uint stateInt; string taskName; string taskDescription; function Task() { owner = msg.sender; stateMessage = \"Task uploaded\"; stateInt = 1; } /** * Set the details specific to this task */ function setUpTaskDetails(uint reward, string name, string description) returns (string){ if(reward >= ((this.balance + msg.value) / 1000000000000000000)) { msg.sender.send(msg.value); return \"Not enough ether sent as reward\"; } taskReward = reward; stateMessage = \"Task details set\"; taskName = name; taskDescription = description; stateInt = 2; return stateMessage; } /** * Complete the task contract */ function completeTask() returns (string){ completedBy = msg.sender; completedBy.send(taskReward * 1 ether); stateInt = 3; stateMessage = \"Task successfully completed\"; return stateMessage; } function deleteTask() returns (string){ owner.send(this.balance); stateInt = 4; stateMessage = \"Deleted Task\"; return stateMessage; } }";
        private BlockAppsClient BlockAppsClient { get; set; }
        private User TaskUser { get; set; }

        public TodoContractClient(string blocUrl, string stratoUrl)
        {
            BlockAppsClient = new BlockAppsClient(blocUrl, stratoUrl);
        }

        public async Task<User> RegisterUser(string username, string password)
        {
            TaskUser =  await BlockAppsClient.UserManager.CreateUser(username, password);
            return TaskUser;
        }

        public async Task<User> SetUser(string username, string password)
        {
            TaskUser =  await BlockAppsClient.UserManager.GetUser(username, password);
            return TaskUser;
        }

        public async Task<List<TodoItem>> GetItems()
        {
            var taskContracts = await TaskUser.BoundContractManager.GetBoundContractsWithName("Task");

            return taskContracts.Where(y => !(y.Properties["stateInt"].Equals("4") || y.Properties["stateInt"].Equals("1")))
                .Select(x => new TodoItem
            {
                Done = x.Properties["stateInt"].Equals("3"),
                ID = x.Address,
                Name = x.Properties["taskName"],
                Notes = x.Properties["taskDescription"],
                Reward = Int32.Parse(x.Properties["taskReward"])
            }).ToList();
        }

        public async Task<TodoItem> GetItem(string id)
        {
            var task = await TaskUser.BoundContractManager.GetBoundContract("Task", id);

            //task has been deleted
            if (task.Properties["stateInt"].Equals("4") || task.Properties["stateInt"].Equals("1"))
            {
                return null;
            }
            var todo = new TodoItem
            {
                Done = task.Properties["stateInt"].Equals("3"),
                ID = task.Address,
                Name = task.Properties["taskName"],
                Notes = task.Properties["taskDescription"],
                Reward = Int32.Parse(task.Properties["taskReward"])
            };

            return todo;

        }

        public async Task<string> SaveItem(TodoItem item)
        {
            BoundContract task;

            if (item.ID == null)
            {
                task = await TaskUser.BoundContractManager.CreateBoundContract(TaskContract, "Task", TaskUser.DefaultAccount);
            }
            else
            {
                task = await TaskUser.BoundContractManager.GetBoundContract("Task", item.ID);
            }

            if (item.Done)
            {
                return await task.CallMethod("completeTask", new Dictionary<string, string>(), 0);
            }

            if (task.Properties["stateInt"].Equals("2"))
            {
                await task.CallMethod("completeTask", new Dictionary<string, string>(), 0);
            }
            var args = new Dictionary<string, string>();
            args.Add("reward", item.Reward.ToString());
            args.Add("name", item.Name);
            args.Add("description", item.Notes);
            return await task.CallMethod("setUpTaskDetails", args, item.Reward);
        }

        public async Task<string> DeleteItem(string todoId)
        {
            var task = await TaskUser.BoundContractManager.GetBoundContract("Task", todoId);
            var args = new Dictionary<string, string>();
            return await task.CallMethod("deleteTask", args, 0);
        }
    }
}
