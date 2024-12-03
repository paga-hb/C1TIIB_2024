using System;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Todo.Blazor.BDDTests.Models;
using Todo.Blazor.BDDTests.Pages;
using Xunit.Abstractions;

namespace Todo.Blazor.BDDTests.Steps;

[Binding]
public class NewTodoItemSteps
{
	private readonly ScenarioContext _scenarioContext;
	private readonly ITestOutputHelper _output;      // Here we are dependency injecting services from SpecFlow's Service Container
	private readonly HomePage _homePage;             //        ||                  ||                  ||
	private readonly EditPage _editPage;             //        \/                  \/                  \/
	
	public NewTodoItemSteps(ScenarioContext scenarioContext, ITestOutputHelper output, HomePage homePage, EditPage editPage)
	{
		_scenarioContext = scenarioContext; // <--- Used as a dictionary for sharing state between steps in the same Sceanrio 
		_output = output;                   // <--- xUnit's helper class with a WriteLine() method for outputting text from tests
		_homePage = homePage;               // <--- Our HomePage instance
		_editPage = editPage;               // <--- Our EditPage instance
	}

	[Given(@"the user is on the home page and clicks the New TodoItem button")] // <--- Maps to the "Given" step in our Feature file
	public async Task GiventheuserisonthehomepageandclickstheNewTodoItembutton()
	{
		// Navigate to the HomePage
		await _homePage.NavigateAsync();
		
		// Store the last TodoItem in the HomePage's HTML table in _scenarioContext (we'll use this in the "Then" step)
		(int id, TodoItem todoItem) = await _homePage.GetLastTodoItemInTableAsync();
		_scenarioContext.Set(id, "Id_Before");
		_scenarioContext.Set(todoItem, "TodoItem_Before");
        
		// Click the "New TodoItem" HTML buttom on the Home Page
		await _homePage.ClickNewTodoItemButtonAsync();
	}

	[When(@"the user provides valid new todo item details and clicks the Save button")] // <--- Maps to the "When" step in our Feature file
	public async Task WhentheuserprovidesvalidnewtodoitemdetailsandclickstheSavebutton(Table table) // <--- A SpecFlow "Table"
	{
		TodoItem todoItem = table.CreateInstance<TodoItem>(); // <--- Convert the Table to a TodoItem

		// Store the new TodoItem in _scenarioContext (we'll use this in the "Then" step)
		_scenarioContext.Set(todoItem, "TodoItem_New");
		
		// Fill in the TodoItem details in the HTML form on the Edit Page
		await _editPage.EnterTaskName(todoItem.TaskName);
		await _editPage.EnterNotes(todoItem.Notes);
		await _editPage.EnterDone(todoItem.Done);

		// Click the "Save" button on the Home Page
		await _editPage.ClickSaveButton();
	}

	[Then(@"the user should be taken to the home page and see the new TodoItem")] // <--- Maps to the "Then" step in our Feature file
	public async Task ThentheusershouldbetakentothehomepageandseethenewTodoItem()
	{
		// Navigate to the Home Page
		await _homePage.NavigateAsync();
		
		// Get the last TodoItem in the HTML table on the Home Page
		(int id_After, TodoItem todoItem_After) = await _homePage.GetLastTodoItemInTableAsync();
		
		// Retrieve the previously last TodoItem, and the new TodoItem, from _scenarioContext (these were stored in the "Given" and "When" steps above)
		_scenarioContext.TryGetValue("Id_Before", out int id_Before);
		_scenarioContext.TryGetValue("TodoItem_Before", out TodoItem todoItem_Before);
		_scenarioContext.TryGetValue("TodoItem_New", out TodoItem todoItem_New);
		
		// Assert the Ids of the last TodoItem in the table before/after adding the new TodoItem are different (i.e. a new row was added)
		id_After.Should().NotBe(id_Before);
		
		// Assert the new TodoItem and the last TodoItem in the table have the same property values
		// (i.e. make sure the TodoItem entered in the "When" step has the same property values as the TodoItem in the Home Page's HTML table)
		todoItem_After.TaskName.Should().Be(todoItem_New.TaskName);
		todoItem_After.Done.Should().Be(todoItem_New.Done);
	}
}