using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Todo.Blazor.BDDTests.Drivers;
using Todo.Blazor.BDDTests.Pages;

namespace Todo.Blazor.BDDTests.Hooks;

[Binding] // <----------------------------------------------------------- The class has a [Binding] attribute so that SpecFlow can find it
public class Hook
{
    [BeforeScenario] // <------------------------------------------------ The [BeforeScenario] attribute marks a ...
    public async Task BeforeTodoItemScenario(IObjectContainer container)  // ... method that should run before each Scenario
    {
        var driver = new Driver(); // <---------------------------------- Create an instance of the Driver (Test Fixture)
        await driver.InitializeAsync(); // <----------------------------- Initialize the Driver (sets up up PLaywright)
        IPage page = await driver.ChromiumBrowser.NewPageAsync(); // <--- Use the Chromium Browser binary

        var homePage = new HomePage(page, driver.BaseUrl); // <---------- Create a class with Playwright commands for the HomePage
        var editPage = new EditPage(page, driver.BaseUrl); // <---------- Create a class with Playwright commands for the EditPage

        container.RegisterInstanceAs(driver); // <----------------------- Register the Driver as a service in SpecFlow's Service Container 
        container.RegisterInstanceAs(homePage); // <--------------------- Register the HomePage as a service in SpecFlow's Service Container
        container.RegisterInstanceAs(editPage); // <--------------------- Register the EditPage as a service in SpecFlow's Service Container
    }

    [AfterScenario] // <------------------------------------------------ The [AfterScenario] attribute marks a ...
    public async Task AfterScenario(IObjectContainer container)          // ... method that should run after each Scenario
    {
        var driver = container.Resolve<Driver>(); // <------------------ Get the Driver from SpecFlow's Service Container
        await driver.DisposeAsync(); // <------------------------------- Dispose of the Driver
    }
}