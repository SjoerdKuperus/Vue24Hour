using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vue24Hour.Models;
using Vue24Hour.Services;

namespace Vue24Hour.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // Handles GET /api/todo
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var userId = "123"; // TODO: Get actual user ID
            var todos = await _todoItemService.GetItems(userId);

            return Ok(todos);
        }

        // POST /api/todo
        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody]TodoItemModel newTodo)
        {
            if (string.IsNullOrEmpty(newTodo?.Text)) return BadRequest();

            await _todoItemService.AddItem("123", newTodo.Text);

            return Ok();
        }

        // POST /api/todo/{id}
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, [FromBody]TodoItemModel updatedData)
        {
            await _todoItemService.UpdateItem("123", id, updatedData);

            return Ok();
        }

        // DELETE /api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            try
            {
                await _todoItemService.DeleteItem("123", id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
