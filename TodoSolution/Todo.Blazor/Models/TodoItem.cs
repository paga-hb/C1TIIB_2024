using System.ComponentModel.DataAnnotations;

namespace Todo.Blazor.Models;

public class TodoItem
{
    public int Id { get; set; }
    [Required(ErrorMessage = "The TaskName field is required."), StringLength(25)]
    public string TaskName { get; set; } = null!;
    [Required(ErrorMessage = "The Notes field is required."), StringLength(500)]
    public string Notes { get; set; } = null!;
    public bool Done { get; set; }
}