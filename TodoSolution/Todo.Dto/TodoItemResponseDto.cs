namespace Todo.Dto;

public class TodoItemResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public bool Done { get; set; }
}