using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public List<TodoItemModel> TodoItemModels { get; set; }

        public FakeTodoItemService()
        {
            var todos = new[]
            {
                new TodoItemModel {Text = "Learn Vue.js", Completed = true, Id = Guid.NewGuid()},
                new TodoItemModel {Text = "Learn ASP.NET Core", Id = Guid.NewGuid()}
            };
            TodoItemModels = todos.ToList();
        }

        public Task<IEnumerable<TodoItemModel>> GetItems(string userId)
        {
            return Task.FromResult(TodoItemModels.AsEnumerable());
        }

        public Task AddItem(string userId, string text)
        {
            TodoItemModels.Add(new TodoItemModel()
            {
                Completed = false,
                Text = text,
                Id = Guid.NewGuid()
            });
            return Task.CompletedTask;
        }

        public Task DeleteItem(string userId, Guid id)
        {
            TodoItemModels.Remove(TodoItemModels.Single(i => i.Id == id));
            return Task.CompletedTask;
        }

        public Task UpdateItem(string userId, Guid id, TodoItemModel updatedData)
        {
            var item = TodoItemModels.Single(i => i.Id == id);
            item.Completed = updatedData.Completed;
            return Task.CompletedTask;
        }
    }
}
