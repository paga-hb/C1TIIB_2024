using Todo.Blazor.Models;

namespace Todo.Blazor.Services;

public interface IRestService
{
    Task<List<TodoItem>> GetTodoItemsAsync();
    Task<TodoItem> GetTodoItemAsync(int id);
    Task AddTodoItemAsync(TodoItem todoItem);
    Task UpdateTodoItemAsync(TodoItem todoItem);
    Task DeleteTodoItemAsync(int id);
}