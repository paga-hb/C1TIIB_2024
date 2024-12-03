using System.Data.Common;
using Microsoft.Playwright;
using Todo.Blazor.BDDTests.Models;

namespace Todo.Blazor.BDDTests.Pages;

public class HomePage
{
    private readonly IPage _page;
    private readonly string _url;

    public HomePage(IPage page, string baseUrl) // <--- This is what we passed in from our Hook.cs file
    {
        _page = page;
        _url = baseUrl;
    }
    
    // These are just Playwright commands we have seen before, but now we have collected all commands for the HomePage in one file
    //                     ||                        ||                              ||
    //                     \/                        \/                              \/

    public async Task NavigateAsync() => await _page.GotoAsync(_url);
    public async Task ClickNewTodoItemButtonAsync() => await _page.GetByRole(AriaRole.Button, new() { Name = "New TodoItem" }).ClickAsync();
    public async Task ClickEditTodoItemButtonAsync() => await _page.GetByRole(AriaRole.Button, new() { Name = "New TodoItem" }).ClickAsync();
    public async Task<(int, TodoItem)> GetLastTodoItemInTableAsync()
    {
        int id = 0;
        TodoItem todoItem = new TodoItem();
        var rows = await _page.Locator($"table tbody tr").AllAsync();
        if (rows.Count > 0)
        {
            var lastRow = rows[^1];
            var cells = lastRow.Locator("td");
            string idString = await cells.Nth(0).TextContentAsync() ?? string.Empty;
            string taskName = await cells.Nth(1).TextContentAsync() ?? string.Empty;
            string done = await cells.Nth(2).TextContentAsync() ?? string.Empty;

            id = int.Parse(idString);
            todoItem.TaskName = taskName;
            todoItem.Done = done != string.Empty && bool.Parse(done);
        }
        return (id, todoItem);
    }
}