using System.Text.Json;
using Microsoft.Playwright;
using FluentAssertions;
using Todo.Dto;
using Xunit.Abstractions;

namespace Todo.APi.IntegrationTests;

public class TodoItemsControllerTests : IClassFixture<PlaywrightFixture>
{
    private readonly IAPIRequestContext _apiContext;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ITestOutputHelper _output;

    public TodoItemsControllerTests(PlaywrightFixture fixture, ITestOutputHelper output)
    {
        _apiContext = fixture.ApiContext;
        _output = output;
        _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
    }

    [Fact]
    public async Task Post_TodoItem_Should_Create_New_TodoItem()
    {
        // Arrange
        var newTodoItem = new TodoItemRequestDto { Name = "Test Name", Notes = "Test Nodes", Done = false };
        _output.WriteLine($"TodoItemRequestDto: Name={newTodoItem.Name}, Notes={newTodoItem.Notes}, Done={newTodoItem.Done}");

        // Act
        var response = await _apiContext.PostAsync("/api/todoitems", new APIRequestContextOptions { DataObject = newTodoItem });
        
        // Assert
        response.Status.Should().Be(201);

        var createdTodoItem = JsonSerializer.Deserialize<TodoItemResponseDto>(await response.TextAsync(), _serializerOptions);
        createdTodoItem.Should().NotBeNull();
        ArgumentNullException.ThrowIfNull(createdTodoItem);

        createdTodoItem.Should().BeOfType<TodoItemResponseDto>();
        createdTodoItem.Name.Should().Be(newTodoItem.Name);
        createdTodoItem.Notes.Should().Be(newTodoItem.Notes);
        createdTodoItem.Done.Should().Be(newTodoItem.Done);
        _output.WriteLine($"TodoItemResponseDto: Id={createdTodoItem.Id}, Name={createdTodoItem.Name}, " +
                          $"Notes={createdTodoItem.Notes}, Done={createdTodoItem.Done}");
    }
}