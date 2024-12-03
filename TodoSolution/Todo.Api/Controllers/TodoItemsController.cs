using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Dto;
using Todo.Data.Entities;
using Todo.Data.Repositories;
using Todo.Data;
using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Controllers;

public enum ErrorCode
{
    TodoItemNameAndNotesRequired,
    TodoItemIDInUse,
    RecordNotFound,
    CouldNotCreateItem,
    CouldNotUpdateItem,
    CouldNotDeleteItem
}

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TodoItemsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_mapper.Map<IEnumerable<TodoItemResponseDto>>(await _unitOfWork.TodoItems.FindAsync()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        TodoItem? todoItem = await _unitOfWork.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
        
        if (todoItem is null)
            return NotFound(ErrorCode.RecordNotFound.ToString());
        
        return Ok(_mapper.Map<TodoItemResponseDto>(todoItem));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoItemRequestDto dto)
    {
        TodoItem item;
        try
        {
            item = _mapper.Map<TodoItem>(dto);

            if (item == null || !ModelState.IsValid)
                return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());

            // This would be used if the TodoItem's Id was included in TodoItemRequestDto
            // if (await _unitOfWork.TodoItems.FirstOrDefaultAsync(t => t.Id == item.Id) != null)
            //     return Conflict(ErrorCode.TodoItemIDInUse.ToString());

            _unitOfWork.TodoItems.Add(item);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
        }
        return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, _mapper.Map<TodoItemResponseDto>(item));

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoItemRequestDto dto)
    {
        TodoItem item;
        TodoItem? existingItem;
        try
        {
            item = _mapper.Map<TodoItem>(dto);

            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
            }

            existingItem = await _unitOfWork.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (existingItem == null)
            {
                return NotFound(ErrorCode.RecordNotFound.ToString());
            }

            existingItem.Name = item.Name;
            existingItem.Notes = item.Notes;
            existingItem.Completed = item.Completed;
            
            _unitOfWork.TodoItems.Update(existingItem);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
        }
        //return NoContent();
        return Ok(_mapper.Map<TodoItemResponseDto>(existingItem));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        TodoItem? item;
        try
        {
            item = await _unitOfWork.TodoItems.FirstOrDefaultAsync(t => t.Id == id);

            if (item == null)
            {
                return NotFound(ErrorCode.RecordNotFound.ToString());
            }

            _unitOfWork.TodoItems.Remove(item);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception)
        {
            return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
        }
        return NoContent();
    }
}