using Microsoft.Playwright;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace Todo.Blazor.EndToEndTests;

public class TodoAppTests : IClassFixture<PlaywrightFixture>
{
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;
    private readonly IPlaywright _playwright;
    private readonly IBrowser _chromiumBrowserBrowser;
    private readonly IBrowser _firefoxBrowserBrowser;
    private readonly IBrowser _webKitBrowserBrowser;
    private readonly ITestOutputHelper _output;

    public TodoAppTests(PlaywrightFixture fixture, ITestOutputHelper output)
    {
        _output = output;
        _configuration = fixture.Configuration;
        _baseUrl = fixture.BaseUrl;
        _playwright = fixture.Playwright;
        _chromiumBrowserBrowser = fixture.ChromiumBrowser;
        _firefoxBrowserBrowser = fixture.FirefoxBrowser;
        _webKitBrowserBrowser = fixture.WebkitBrowser;
    }

    [Fact]
    public async Task HomePage_NavigateTo_NewTodoItem_ThenEditAndSave_ShouldContainNewTodoItemInHomePageTodoItemList()
    {
        // Add testing logic here

        // Open a new browser
        var context = await _chromiumBrowserBrowser.NewContextAsync();  // Use this if you want to test in a Chromium-based browser
        //var context = await _firefoxBrowserBrowser.NewContextAsync(); // Use this if you want to test in a Firefox-based browser
        //var context = await _webKitBrowserBrowser.NewContextAsync();  // Use this if you want to test in a WebKit-based browser

         // Create a Page object (we can use this to navigate to different URLs, and interact with HTML elements on web pages)
        var page = await context.NewPageAsync();

        // Navigate to the Home Page
        await page.GotoAsync(_baseUrl);

        // Assert the title contains text "Todo List" <--- Added this assertion
        var title = await page.TitleAsync();
        title.Should().Contain("Todo List");

        // Get the number of <td> HTML elements on the page containing the text "Yakety Yak" <--- Added this
        var countBefore = await page.GetByRole(AriaRole.Cell, new() { Name = "Yakety Yak" }).CountAsync();

        // Click the New TodoItem button
        await page.GetByRole(AriaRole.Button, new() { Name = "New TodoItem" }).ClickAsync();

        // Assert the edit page has a <h3> HTML element with the text "New TodoItem" <--- Added this assertion
        var heading = await page.InnerTextAsync("h3");
        heading.Should().Be("New TodoItem");

        // Click the Name input field
        await page.GetByLabel("Name").ClickAsync();

        // Fill in the text "Yakety Yak"
        await page.GetByLabel("Name").FillAsync("Yakety Yak");

        // Assert the input field contains the expected text // <--- Added this assertion
        (await page.GetByLabel("Name").InputValueAsync()).Should().Be("Yakety Yak");

        // Click the Notes input field
        await page.GetByLabel("Notes").ClickAsync();

        // Fill in the text "Take out the papers and the trash."
        await page.GetByLabel("Notes").FillAsync("Take out the papers and the trash.");

        // Assert the input field contains the expected text // <--- Added this assertion
        (await page.GetByLabel("Notes").InputValueAsync()).Should().Be("Take out the papers and the trash.");
        
        // Check the Done checkbox
        await page.GetByLabel("Done").CheckAsync();

        // Assert the checkbox is checked // <--- Added this assertion
        (await page.GetByLabel("Done").IsCheckedAsync()).Should().Be(true);

        // Save a screenshot of the new item before saving it (this will be in the project's output folder) // <--- Added this
        await page.ScreenshotAsync(new() { Path = "NewTodoItem.png" });

        // Click the Save button
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        // Navigate to the Home Page // <--- Added this
        await page.GotoAsync(_baseUrl);

        // Get the number of <td> HTML elements on the page containing the text "Yakety Yak" <--- Added this
        var countAfter = await page.GetByRole(AriaRole.Cell, new() { Name = "Yakety Yak" }).CountAsync();

        // Assert the new TodoItem is in the TodoItems list on the Home page <--- Added this assertion
        countAfter.Should().Be(countBefore + 1);
        _output.WriteLine($"CountBefore: {countBefore}, CountAfter: {countAfter}");

        // Clean up (closes the web browser) <--- Added this
        await context.CloseAsync();
    }
}