using Microsoft.Playwright;
using Todo.Blazor.BDDTests.Drivers;

namespace Todo.Blazor.BDDTests.Pages;

public class EditPage
{
    private readonly IPage _page;
    private readonly string _url;

    public EditPage(IPage page, string baseUrl) // <--- This is what we passed in from our Hook.cs file
    {
        _page = page;
        _url = $"{baseUrl}/edittodoitem/";
    }
    
    // These are just Playwright commands we have seen before, but now we have collected all commands for the EditPage in one file
    //                     ||                        ||                              ||
    //                     \/                        \/                              \/

    public async Task NavigateNewTodoItemAsync() => await _page.GotoAsync(_url);
    public async Task NavigateEditTodoItemAsync(int Id) => await _page.GotoAsync($"{_url}/{Id}");
    public async Task EnterTaskName(string taskName) => await _page.GetByLabel("Name").FillAsync(taskName);
    public async Task EnterNotes(string notes) => await _page.GetByLabel("Notes").FillAsync(notes);
    public async Task EnterDone(bool done)
    {
        var doneCheckbox = _page.GetByLabel("Done");
        if(done)
            await doneCheckbox.CheckAsync();
        else
            await doneCheckbox.UncheckAsync();
    }
    public async Task ClickSaveButton() => await _page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
}