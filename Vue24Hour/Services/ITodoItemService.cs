using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vue24Hour.Models;

namespace Vue24Hour.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemModel>> GetItems(string userId);
        Task AddItem(string userId, string text);
        Task UpdateItem(string userId, Guid id, TodoItemModel updatedData);
        Task DeleteItem(string userId, Guid id);
    }
}
