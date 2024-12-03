using FluentAssertions;
using Todo.Dto;
using Todo.Data.Repositories;
using AutoMapper;
using Todo.Api.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Entities;
using Xunit.Abstractions;

namespace Todo.APi.UnitTests;

public class TodoItemsControllerTests
{
    private readonly TodoItem _item1;
    private readonly Mock<ITodoItemRepository> _mockTodoItemRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly TodoItemsController _todoItemsController;
    private readonly ITestOutputHelper _output;

    public TodoItemsControllerTests(ITestOutputHelper output)
    {
        _output = output;

        // Test Fixture

        // Sample data
        _item1 = new TodoItem { Id = 1, Name = "Learn xUnit", Notes = "Explore xUnit", Completed = false, Comments = "xUnit" };
        int Id = _item1.Id;

        // Mock TodoItemRepository 
        _mockTodoItemRepository = new Mock<ITodoItemRepository>();
        _mockTodoItemRepository.Setup(c => c.FirstOrDefaultAsync(t => t.Id == Id)).ReturnsAsync(() => _item1);
        _mockTodoItemRepository.Setup(c => c.Update(It.IsAny<TodoItem>())); // if we did this in the "Arrage" section we could pass in "todoItemOut" to Update()

        // Mock UnitOfWork
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockUnitOfWork.Setup(c => c.TodoItems).Returns(_mockTodoItemRepository.Object);
        _mockUnitOfWork.Setup(c => c.CompleteAsync()).ReturnsAsync(() => It.IsAny<int>()); // we could also pass in "() => 1" to ReturnsAsync()

        // Mock Mapper
        _mockMapper = new Mock<IMapper>();

        // Subject Under Test (SUT)
        _todoItemsController = new TodoItemsController(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Update_ShouldUpdateTodoItem_WhenFound()
    {
        // Arrange
        var request = new TodoItemRequestDto { Name = "Learn nUnit", Notes = "Explore nUnit", Done = true };
        var todoItemIn = new TodoItem { Id = 0, Name = request.Name, Notes = request.Notes, Completed = request.Done, Comments = "" };
        var todoItemOut = new TodoItem { Id = _item1.Id, Name = request.Name, Notes = request.Notes, Completed = request.Done, Comments = _item1.Comments };
        var expected = new TodoItemResponseDto { Id = todoItemOut.Id, Name = todoItemOut.Name, Notes = todoItemOut.Notes, Done = todoItemOut.Completed };

        _mockMapper.Setup(m => m.Map<TodoItem>(request)).Returns(todoItemIn);
        _mockMapper.Setup(m => m.Map<TodoItemResponseDto>(It.IsAny<TodoItem>())).Returns(expected); // todoItemOut

        // Act
        IActionResult result = await _todoItemsController.Update(_item1.Id, request);

        // Assert

        // Verify correct status code 
        OkObjectResult ok = (OkObjectResult)result;
        ok.StatusCode.Should().Be(200);

        // Verify correct response
        TodoItemResponseDto actual = (TodoItemResponseDto)ok.Value!;
        actual.Id.Should().Be(expected.Id);
        actual.Name.Should().Be(expected.Name);
        actual.Notes.Should().Be(expected.Notes);
        actual.Done.Should().Be(expected.Done);
        _output.WriteLine($"Update({_item1.Id}): Id={actual.Id}, Name={actual.Name}, Notes={actual.Notes}, Done={actual.Done}");

        // Verify UnitOfWork.TodoItems.Add() and UnitOfWork.CompleteAsync() were called exactly once
        _mockUnitOfWork.Verify(c => c.TodoItems.Update(It.IsAny<TodoItem>()), Times.Once());
        _mockUnitOfWork.Verify(c => c.CompleteAsync(), Times.Once());
    }
}