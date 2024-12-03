namespace Todo.Dto;

public class TodoItemRequestDto
{
    public string Name { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public bool Done { get; set; }
}